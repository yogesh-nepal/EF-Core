using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EFmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EFcoreMVC.Controllers
{
    [Authorize]
    public class MediaController : Controller
    {
        private readonly IHttpClientFactory client;
        public MediaController(IHttpClientFactory _client)
        {
            client = _client;
        }

        [HttpGet]
        public IActionResult AddMedia()
        {
            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1024000000)]
        [RequestSizeLimit(1024000000)]
        public IActionResult AddMedia(IFormFile mediaUpload)
        {
            mediaUpload = Request.Form.Files.FirstOrDefault();
            var formViewData = Request.Form["dataObj"];
            var jsonObj = JsonConvert.DeserializeObject(formViewData);
            if (mediaUpload != null)
            {
                byte[] bt;
                using (BinaryReader br = new BinaryReader(mediaUpload.OpenReadStream()))
                {
                    bt = br.ReadBytes((int)mediaUpload.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(bt);
                MultipartFormDataContent formDataContent = new MultipartFormDataContent();
                var jsonObject = JsonConvert.SerializeObject(jsonObj);
                var stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                formDataContent.Add(bytes, "video", mediaUpload.FileName);
                formDataContent.Add(stringContent, "formData");
                var _http = client.CreateClient("apiClient");

                /* token from cookie */
                var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
                _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", cookieToken);
                var response = _http.PostAsync("Media/AddMedia", formDataContent).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("ShowPosts", "Post");
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Access");
                }
            }
            return RedirectToAction("AccessDenied", "Access");
        }

        [HttpGet]
        public IActionResult ShowMedia()
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            IEnumerable<MediaPostModel> mediaList;
            var _http = client.CreateClient("apiClient");
            var response = _http.GetAsync("Media/GetAllMedia").Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                mediaList = response.Content.ReadAsAsync<IEnumerable<MediaPostModel>>().Result;
                return View(mediaList);
            }
            return RedirectToAction("AccessDenied", "Access");
        }
    }
}