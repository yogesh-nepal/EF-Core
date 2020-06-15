using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EFcoreBL.Interface;
using EFmodels;
using EFmodels.Resource;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMultipleImageData imageData;
        private readonly IWebHostEnvironment env;
        private readonly IMapper _map;
        public PostsController(IPosts _iposts, IMultipleImageData _imageData,
        IWebHostEnvironment _env,
        IMapper _map)
        {
            this._map = _map;
            this.iposts = _iposts;
            this.imageData = _imageData;
            this.env = _env;
        }

        [HttpPost("AddPosts")]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult AddPosts([FromForm] APostWithMultipleImageModel model)
        {
            List<MultipleImageData> multipleImageDataList = new List<MultipleImageData>();
            var files = HttpContext.Request.Form.Files;
            var formData = Request.Form["formData"];
            var mpModel = JsonConvert.DeserializeObject<APostWithMultipleImageModel>(formData);
            // mpModel.ImagePath = new List<string>();
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
                    // mpModel.ImagePath.Add(fileName);
                    multipleImageDataList.Add(multipleImageData);
                    imageData.InsertIntoMultipleImageDataTable(multipleImageData);
                    imageData.Save();
                }
            }
            mpModel.MultipleImageData = multipleImageDataList;
            iposts.InsertIntoPostsTable(mpModel);
            iposts.Save();
            return Ok(HttpStatusCode.OK);
        }

        [HttpGet("ShowMultipleImagePosts")]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult ShowMultipleImagePosts()
        {
            var list = iposts.GetPostWithFiles().GetAwaiter().GetResult();
            var reso = _map.Map<List<PostWithMultipleImageResource>>(list);
            return Ok(reso);
        }

        [HttpDelete("DeletePosts/{id}")]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult DeletePosts(int id)
        {
            iposts.DeleteFromPostsTable(id);
            iposts.SavePostsTable();
            return Ok(HttpStatusCode.OK);
        }

        [HttpGet("GetPostsByID/{id}")]
        [Authorize(Roles="Admin,Author")]
        public IActionResult GetPostByID(int id)
        {
            var data = iposts.GetDataFromId(id);
            return Ok(data);
        }
    }
}