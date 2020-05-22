using System.Security.AccessControl;
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
    public class UserWithRoleController : ControllerBase
    {
        private readonly IUserWithRole iUserRole;
        public UserWithRoleController(IUserWithRole _iUserRole)
        {
            this.iUserRole = _iUserRole;
        }

        [HttpPost("Insert")]
        [Authorize(Roles="Admin")]
        public IActionResult Insert(UserWithRoleModel model)
        {
            iUserRole.InsertRoleForUser(model);
            iUserRole.Save();
            return Ok(HttpStatusCode.OK);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles="Admin")]
        public IActionResult Delete(int id)
        {
            iUserRole.DeleteRoleForUser(id);
            iUserRole.Save();
            return Ok(HttpStatusCode.OK);
        }
    }
}