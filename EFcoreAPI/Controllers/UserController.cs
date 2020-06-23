using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Net.Cache;
using System.Net;
using EFcoreBL.Interface;
using EFmodels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EFcoreDAL;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Extensions.Caching.Memory;

namespace EFcoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;
        private readonly IMemoryCache _cache;
        public IConfiguration configuration;

        public UserController(IUser user, IConfiguration _configuration, IMemoryCache cache)
        {
            this.user = user;
            this.configuration = _configuration;
            this._cache = cache;
        }

        [HttpPost("Insert")]
        [Authorize(Roles = "Admin")]
        public IActionResult Insert(UserModel uModel)
        {
            var data = user.GetAllFromTable();
            var dataexixts = data.Where(x => x.UserEmailID == uModel.UserEmailID).FirstOrDefault();
            if (dataexixts != null)
            {
                return BadRequest();
            }
            user.InsertIntoTable(uModel);
            user.Save();
            return Ok(HttpStatusCode.OK);
        }
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(UserModel uModel)
        {
            /* To update specific fields */
            // UserModel um = user.GetDataFromId(uModel.UserID.GetValueOrDefault());
            // um.UserFullName = uModel.UserFullName;
            // user.UpdateTable(um);

            /* To update the whole table */
            user.UpdateTable(uModel);
            user.Save();
            return Ok(HttpStatusCode.OK);
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            if (_cache.TryGetValue("Get-All-Users", out IEnumerable<UserModel> list))
            {
                return Ok(list);
            }
            list = user.GetAllFromTable();
            _cache.Set("Get-All-Users", list, TimeSpan.FromDays(7));
            return Ok(list);
        }

        [HttpGet("ShowAll")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            if (_cache.TryGetValue("show-key", out IEnumerable<UserModel> list))
            {
                return Ok(list);
            }
            list = user.GetUserWithRole();
            _cache.Set("show-key", list, TimeSpan.FromDays(7));
            return Ok(list);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            user.DeleteFromTable(id);
            user.Save();
            return Ok(HttpStatusCode.OK);
        }

        [HttpPost("UserLogin")]
        public IActionResult userLogin(UserModel uModel)
        {
            var loginData = user.UserLogin(uModel.UserEmailID, uModel.UserPassword);
            if (loginData != null)
            {
                var _token = GenerateToken(loginData);

                /* returns role from claim */

                // var token = new JwtSecurityTokenHandler().ReadJwtToken(_token);
                // var claim = token.Claims.First(c => c.Type == ClaimTypes.Role).Value; //returns only first role
                // var claim = token.Claims.Where(c => c.Type == ClaimTypes.Role);
                // foreach (var item in claim)
                // {
                //     var role = item.Value;
                // }

                return Ok(_token);
            }
            return NotFound();
        }

        public string GenerateToken(UserModel userModel)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtProp:key"]));
            var alg = SecurityAlgorithms.HmacSha256;
            var credentials = new SigningCredentials(key, alg);
            var listOfRoles = userModel.ListOfRoles;

            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userModel.UserEmailID),
                new Claim("usrPassword",userModel.UserPassword),
                new Claim("usrName",userModel.UserFullName)
            };
            if (listOfRoles.Count > 0)
            {
                foreach (var role in listOfRoles)
                {
                    claim.Add(new Claim(ClaimTypes.Role, role.RoleName));
                }
            }

            var _securityToken = new JwtSecurityToken(
                issuer: configuration["jwtProp:validIssuer"],
                audience: configuration["jwtProp:validAudience"],
                expires: DateTime.Now.AddHours(2),
                notBefore: DateTime.Now,
                signingCredentials: credentials,
                claims: claim
            );
            var _token = new JwtSecurityTokenHandler().WriteToken(_securityToken);
            return _token;
        }

        [HttpPost("FromCSV")]
        [Authorize(Roles = "Admin")]
        public IActionResult FromCSV([FromForm] UserModel model)
        {
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            using (TextFieldParser csvParser = new TextFieldParser(file.OpenReadStream()))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;
                // Skip the row with the column names
                csvParser.ReadLine();
                List<UserModel> uList = new List<UserModel>();
                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    var data = user.GetAllFromTable();
                    var dataExists = data.Where(x => x.UserEmailID == fields[1]).FirstOrDefault();
                    if (dataExists == null)
                    {
                        uList.Add(new UserModel
                        {
                            UserFullName = fields[0],
                            UserEmailID = fields[1],
                            UserPassword = fields[2],
                            UserAddress = fields[3],
                            UserGender = fields[4],
                            UserPhone = fields[5],
                            IsActive = bool.Parse(fields[6])
                        });
                    }
                }
                user.InsertFromCSV(uList);
                user.Save();
            }
            return Ok(HttpStatusCode.OK);
        }
    }
}