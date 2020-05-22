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
            role.InsertIntoTable(rModel);
            role.Save();
            return Ok(HttpStatusCode.OK);
        }

        [HttpGet("ShowAll")]
        [Authorize(Roles = "Admin")]
        public IActionResult ShowAll()
        {
           var list = role.GetAllFromTable();
           return Ok(list);
        }
    }
}