using System.Security.AccessControl;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFcoreBL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EFmodels;
using Microsoft.AspNetCore.Authorization;

namespace EFcoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMenuController : ControllerBase
    {
        private readonly IUserMenu iUserMenu;
        private readonly IUser iUser;
        public UserMenuController(IUserMenu _iUserMenu, IUser _iUser)
        {
            this.iUserMenu = _iUserMenu;
            this.iUser = _iUser;
        }

        [HttpPost("Insert")]
        [Authorize(Roles = "Admin")]
        public IActionResult Insert(UserMenuModel model)
        {
            iUserMenu.InsertIntoTable(model);
            iUserMenu.Save();
            return Ok(HttpStatusCode.OK);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            iUserMenu.DeleteMenuForUser(id);
            iUserMenu.Save();
            return Ok(HttpStatusCode.OK);
        }
        [HttpGet("ShowUserMenu")]
        public IActionResult ShowUserMenu()
        {
            var list = iUser.ShowUserMenu();
            return Ok(list);

        }
    }
}