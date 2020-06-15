using System.Text;
using System.Net.Cache;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EFmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
<<<<<<< HEAD
using System.Security.Claims;
=======
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953

namespace EFcoreMVC.Controllers
{

    [Authorize]
    public class PostsController : Controller
    {
        private IHttpClientFactory client;
        public PostsController(IHttpClientFactory _client)
        {
            client = _client;
        }

        [HttpGet]
        public IActionResult AddPosts()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPosts(IEnumerable<IFormFile> imageUpload)
        {
            /** TODO **/
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            imageUpload = Request.Form.Files;
            var formViewData = Request.Form["dataObj"];
            var jsonObj = JsonConvert.DeserializeObject(formViewData);
            MultipartFormDataContent formData = new MultipartFormDataContent();
            foreach (var file in imageUpload)
            {
                if (file != null)
                {
                    byte[] bt;
                    using (BinaryReader br = new BinaryReader(file.OpenReadStream()))
                    {
                        bt = br.ReadBytes((int)file.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(bt);
                    formData.Add(bytes, "img", file.FileName);
                }
            }
            var jsonObject = JsonConvert.SerializeObject(jsonObj);
            var stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            formData.Add(stringContent, "formData");
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.PostAsync("Posts/AddPosts", formData).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
<<<<<<< HEAD
                return RedirectToAction("AllPosts", "Posts");
=======
                return RedirectToAction("ShowPost", "Post");
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
            }
            else
            {
                return RedirectToAction("AccessDenied", "Access");
            }
        }
<<<<<<< HEAD

        [HttpGet]
        public IActionResult AllPosts()
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            IEnumerable<APostWithMultipleImageModel> postList;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.GetAsync("Posts/ShowMultipleImagePosts").Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                postList = response.Content.ReadAsAsync<IEnumerable<APostWithMultipleImageModel>>().Result;
                return View(postList);
            }
            return RedirectToAction("AccessDenied", "Access");
        }

        [HttpGet]
        public IActionResult DeletePosts(int id)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var responseImageData = _http.DeleteAsync("MultipleImageData/DeleteMultipleImage/"+id).Result;
            var response = _http.DeleteAsync("Posts/DeletePosts/" + id).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("AllPosts", "Posts");
            }
            else
            {
                return RedirectToAction("PageNotFound", "Access");
            }
        }

        [HttpGet]
        public IActionResult GetMultipleImagePostByID(int id)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var cookieRole = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role);
            foreach (var item in cookieRole)
            {
                var role = item.Value;
                if (role == "Admin" || role == "Author")
                {
                    var _http = client.CreateClient("apiClient");
                    _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", cookieToken);
                    var response = _http.GetAsync("Posts/GetPostsByID/" + id).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var list = response.Content.ReadAsAsync<APostWithMultipleImageModel>().Result;
                        return View(list);
                    }
                }
            }
            return RedirectToAction("AccessDenied", "Access");
        }
=======
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
    }
}