using System.Diagnostics;
using System.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EFmodels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EFcoreBL.Interface;
using Microsoft.AspNetCore.Authorization;

namespace EFcoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {

        private readonly IWebHostEnvironment env;
        private readonly IMedia iMedia;
        public MediaController(IWebHostEnvironment _env, IMedia _iMedia)
        {
            this.env = _env;
            this.iMedia = _iMedia;
        }

        [HttpPost("AddMedia")]
        [Authorize(Roles = "Admin,Author")]
        [RequestFormLimits(MultipartBodyLengthLimit = 1024000000)]
        [RequestSizeLimit(1024000000)]
        public IActionResult AddMedia([FromForm] MediaPostModel model)
        {
            var videoFile = HttpContext.Request.Form.Files.FirstOrDefault();
            var formData = Request.Form["formData"];
            var mediaModel = JsonConvert.DeserializeObject<MediaPostModel>(formData);
            if (videoFile != null)
            {
                var dir = Path.Combine(env.WebRootPath, "Videos");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var videoFileName = Guid.NewGuid().ToString() + "" + Path.GetFileName(videoFile.FileName);
                var videoFilePath = Path.Combine(dir, videoFileName);
                using (var stream = new FileStream(videoFilePath, FileMode.Create))
                {
                    videoFile.CopyToAsync(stream).GetAwaiter().GetResult();
                    stream.FlushAsync().GetAwaiter().GetResult();
                }
                /* FOR THUMBNAIL
                    Using FFmpeg
                  */

                var fileExtension = Path.GetExtension(videoFilePath);
                var thumbnailImageName = videoFileName.Replace(fileExtension, ".jpg");
                var thumbnailImagePath = Path.Combine(env.WebRootPath, "Thumbnails", thumbnailImageName);

                ProcessStartInfo startInfo = new ProcessStartInfo();
                string arguments = $"-i {videoFilePath} -ss 00:00:01.000 -vframes 1 {thumbnailImagePath}";
                startInfo.FileName = Path.Combine(env.WebRootPath, "FFmpeg\\ffmpeg.exe");
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardError = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = arguments;
                try
                {
                    Process process = Process.Start(startInfo);
                    process.WaitForExit(5000);
                    process.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                mediaModel.MediaThumbnail = thumbnailImageName;
                mediaModel.MediaPostPath = videoFileName;
                iMedia.InsertIntoMediaTable(mediaModel);
                iMedia.Save();
                return Ok(HttpStatusCode.OK);
            }
            return Ok(HttpStatusCode.NoContent);
        }

        [HttpGet("GetAllMedia")]
        public IActionResult GetAllMedia()
        {
            var mediaData = iMedia.GetAllMedia();
            return Ok(mediaData);
        }

        [HttpDelete("DeleteMedia/{id}")]
        public IActionResult DeleteMedia(int id)
        {
            iMedia.DeleteFromMediaTable(id);
            iMedia.Save();
            return Ok(HttpStatusCode.OK);
        }
    }
}