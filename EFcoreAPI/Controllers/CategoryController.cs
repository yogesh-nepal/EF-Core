using System.Net;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EFcoreBL.Interface;
using EFmodels;
using Microsoft.AspNetCore.Authorization;

namespace EFcoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory iCategory;
        public CategoryController(ICategory _iCategory)
        {
            iCategory = _iCategory;
        }

        [HttpPost("Insert")]
        [Authorize(Roles="Admin,Author")]
        public IActionResult Insert(CategoryModel model)
        {
            iCategory.InsertIntoTable(model);
            iCategory.Save();
            return Ok(HttpStatusCode.OK);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles="Admin")]
        public IActionResult Delete(int id)
        {
            iCategory.DeleteFromTable(id);
            iCategory.Save();
            return Ok(HttpStatusCode.OK);
        }

        [HttpGet("ShowAll")]
        public IActionResult ShowAll()
        {
            var list = iCategory.GetAllFromTable();
            return Ok(list);
        }
    }
}