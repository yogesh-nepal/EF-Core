using System.Net.Http.Headers;
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
    public class MenuController : ControllerBase
    {
        private readonly IMenu imenu;
        public MenuController(IMenu _imenu)
        {
            this.imenu = _imenu;
        }

        [HttpPost("Insert")]
        [Authorize(Roles = "Admin")]
        public IActionResult Insert(MenuModel model)
        {
            imenu.InsertIntoTable(model);
            imenu.Save();
            return Ok(HttpStatusCode.OK);
        }

        [HttpGet("ShowAll")]
        public IActionResult ShowAll()
        {
            var list = imenu.GetAllFromTable();
            return Ok(list);
        }

        [HttpPost("Update")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(MenuModel model)
        {
            imenu.UpdateTable(model);
            imenu.Save();
            return Ok(HttpStatusCode.OK);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            imenu.DeleteFromTable(id);
            imenu.Save();
            return Ok(HttpStatusCode.OK);
        }

        [HttpGet("GetDetails/{id}")]
        public IActionResult GetDetails(int id)
        {
            var list = imenu.GetDataFromId(id);
            return Ok(list);
        }
    }
}