using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFcoreBL.Interface;
using EFmodels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EFcoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole role;

        public RoleController(IRole _role)
        {
            role = _role;
        }

        [HttpPost("Insert")]
        [Authorize(Roles = "Admin")]
        public IActionResult Insert(RoleModel rModel)
        {
            var roleData = role.GetAllFromTable();
            var roleExists = roleData.Where(x => x.RoleName == rModel.RoleName).FirstOrDefault();
            if (roleExists == null)
            {
                role.InsertIntoTable(rModel);
                role.Save();
                return Ok(HttpStatusCode.OK);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("ShowAll")]
        [Authorize(Roles = "Admin")]
        public IActionResult ShowAll()
        {
            var list = role.GetAllFromTable();
            return Ok(list);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            role.DeleteFromTable(id);
            role.Save();
            return Ok(HttpStatusCode.OK);
        }
    }
}