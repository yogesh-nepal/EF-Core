using System.Net;
using EFcoreBL.Interface;
using EFmodels;
using Microsoft.AspNetCore.Mvc;

namespace EFcoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;

        public UserController(IUser user)
        {
            this.user = user;
        }

        [HttpPost("Insert")]
        public IActionResult Insert(UserModel model)
        {
            user.InsertIntoTable(model);
            user.Save();
            return Ok(HttpStatusCode.OK);
        }

        [HttpGet("ShowAll")]
        public IActionResult ShowAll()
        {
            var list = user.GetAllFromTable();
            return Ok(list);
        }

    }
}