using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFcoreBL.Interface;
using EFcoreBL.Repository;
using EFmodels;
using Microsoft.AspNetCore.Http;
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
    }
}