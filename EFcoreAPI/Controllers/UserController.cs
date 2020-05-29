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

namespace EFcoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;
        public IConfiguration configuration;

        public UserController(IUser user, IConfiguration _configuration)
        {
            this.user = user;
            this.configuration = _configuration;
        }

        [HttpPost("Insert")]
        [Authorize(Roles = "Admin")]
        public IActionResult Insert(UserModel uModel)
        {
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
        [Authorize(Roles="Admin")]
        public IActionResult GetAllUsers()
        {
            var list = user.GetAllFromTable();
            return Ok(list);
        }

        [HttpGet("ShowAll")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            var list = user.GetUserWithRole();
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

    }
}