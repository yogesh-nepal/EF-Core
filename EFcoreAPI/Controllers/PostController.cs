using System.Net;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFcoreBL.Interface;
using EFmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace EFcoreAPI.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPost ipost;
        private readonly IWebHostEnvironment env;
        public PostController(IPost _ipost, IWebHostEnvironment _env)
        {
            this.ipost = _ipost;
            this.env = _env;
        }

        [HttpPost("AddPost")]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult AddPost([FromForm] PostModel model)
        {
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            var formData = Request.Form["formData"];
            var pModel = JsonConvert.DeserializeObject<PostModel>(formData);
            if (file != null)
            {
                var dir = Path.Combine(env.WebRootPath, "Resource");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var fileName = Guid.NewGuid().ToString() + "" + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(dir, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(stream).GetAwaiter().GetResult();
                    stream.FlushAsync().GetAwaiter().GetResult();
                }
                pModel.ImageURL = fileName;
                ipost.InsertIntoPostTable(pModel);
                ipost.Save();
                return Ok(HttpStatusCode.OK);
            }
            else
            {
                ipost.InsertIntoPostTable(pModel);
                ipost.Save();
                return Ok(HttpStatusCode.OK);
            }
        }
        [HttpGet("ShowPosts")]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult ShowPost()
        {
            var list = ipost.GetAllFromTable();
            return Ok(list);
        }
        [HttpGet("GetPostById/{id}")]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult GetPostByID(int id)
        {
            var data = ipost.GetDataFromId(id);
            return Ok(data);
        }
        [HttpPost("UpdatePost")]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult UpdatePost([FromForm] PostModel model)
        {
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            var formData = Request.Form["formData"];
            var pModel = JsonConvert.DeserializeObject<PostModel>(formData);
            if (file != null)
            {
                var dir = Path.Combine(env.WebRootPath, "Resource");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var fileName = Guid.NewGuid().ToString() + "" + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(dir, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(stream).GetAwaiter().GetResult();
                    stream.FlushAsync().GetAwaiter().GetResult();
                }
                pModel.ImageURL = fileName;
                ipost.UpdatePostTable(pModel);
                ipost.Save();
                return Ok(HttpStatusCode.OK);
            }
            else
            {
                ipost.UpdatePostTable(pModel);
                ipost.Save();
                return Ok(HttpStatusCode.OK);
            }

        }

        [HttpDelete("DeletePost/{id}")]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult DeletePost(int id)
        {
            ipost.DeleteFromTable(id);
            ipost.Save();
            return Ok(HttpStatusCode.OK);
        }
    }
}