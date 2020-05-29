using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EFcoreBL.Interface;
using EFmodels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EFcoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPosts iposts;
        private readonly IWebHostEnvironment env;
        public PostsController(IPosts _iposts, IWebHostEnvironment _env)
        {
            this.iposts = _iposts;
            this.env = _env;
        }

        [HttpPost("AddPosts")]
        public IActionResult AddPosts([FromForm] APostWithMultipleImageModel model)
        {
            List<MultipleImageData> multipleImageDataList = new List<MultipleImageData>();
            var files = HttpContext.Request.Form.Files;
            var formData = Request.Form["formData"];
            var mpModel = JsonConvert.DeserializeObject<APostWithMultipleImageModel>(formData);
            foreach (var item in files)
            {
                if (item != null)
                {
                    var dir = Path.Combine(env.WebRootPath, "Resource");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    var fileName = Guid.NewGuid().ToString() + "" + Path.GetFileName(item.FileName);
                    var filePath = Path.Combine(dir, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        item.CopyToAsync(stream).GetAwaiter().GetResult();
                        stream.FlushAsync().GetAwaiter().GetResult();
                    }
                    
                    MultipleImageData multipleImageData = new MultipleImageData
                    {
                        ImagePathData = fileName
                    };
                    multipleImageDataList.Add(multipleImageData);
                    
                }
            }
            mpModel.MultipleImageData = multipleImageDataList;
            iposts.InsertIntoPostsTable(mpModel);
            return Ok(HttpStatusCode.OK);
        }
    }
}