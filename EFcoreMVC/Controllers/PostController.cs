using System.IO;
using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using EFmodels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace EFcoreMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IHttpClientFactory client;
        public PostController(IHttpClientFactory _client)
        {
            this.client = _client;
        }

        /* For Category */
        public IActionResult AddCategory()
        {
            var cookieRole = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role);
            foreach (var item in cookieRole)
            {
                var role = item.Value;
                if (role == "Admin" || role == "Author")
                {
                    return View();
                }
            }
            return RedirectToAction("AccessDenied", "Access");
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryModel categoryModel)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.PostAsJsonAsync("Category/Insert", categoryModel).GetAwaiter().GetResult();
            return RedirectToAction("ShowCategory");
        }

        [HttpGet]
        public IActionResult ShowCategory()
        {
            var cookieRole = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role);
            foreach (var item in cookieRole)
            {
                var role = item.Value;
                if (role == "Admin" || role == "Author")
                {
                    IEnumerable<CategoryModel> catList;
                    var _http = client.CreateClient("apiClient");
                    var response = _http.GetAsync("Category/ShowAll").Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        catList = response.Content.ReadAsAsync<IEnumerable<CategoryModel>>().Result;
                        return View(catList);
                    }
                }
            }
            return RedirectToAction("AccessDenied", "Access");
        }

        [HttpGet]
        public IActionResult LoadCategory()
        {
            IEnumerable<CategoryModel> catList;
            var _http = client.CreateClient("apiClient");
            var response = _http.GetAsync("Category/ShowAll").Result;
            catList = response.Content.ReadAsAsync<IEnumerable<CategoryModel>>().Result;
            return Json(catList);
        }
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.DeleteAsync("Category/Delete/" + id).GetAwaiter().GetResult();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("ShowCategory");
            }
            return RedirectToAction("AccessDenied", "Access");
        }

        /* For Post */
        [HttpGet]
        public IActionResult AddPost()
        {
            var cookieRole = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role);
            foreach (var item in cookieRole)
            {
                var role = item.Value;
                if (role == "Admin" || role == "Author")
                {
                    return View();
                }
            }
            return RedirectToAction("AccessDenied", "Access");
        }

        [HttpPost]
        public IActionResult AddPost(IFormFile imageUpload)
        {
            imageUpload = Request.Form.Files.FirstOrDefault();
            var formViewData = Request.Form["dataObj"];
            var jsonObj = JsonConvert.DeserializeObject(formViewData);
            if (imageUpload != null)
            {
                byte[] bt;
                using (BinaryReader br = new BinaryReader(imageUpload.OpenReadStream()))
                {
                    bt = br.ReadBytes((int)imageUpload.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(bt);
                MultipartFormDataContent formDataContent = new MultipartFormDataContent();
                var jsonObject = JsonConvert.SerializeObject(jsonObj);
                var stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                formDataContent.Add(bytes, "img", imageUpload.FileName);
                formDataContent.Add(stringContent, "formData");
                var _http = client.CreateClient("apiClient");
                /* token from cookie */
                var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
                _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", cookieToken);
                var response = _http.PostAsync("Post/AddPost", formDataContent).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("ShowPosts", "Post");
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Access");
                }
            }
            else
            {
                MultipartFormDataContent formDataContent = new MultipartFormDataContent();
                var jsonObject = JsonConvert.SerializeObject(jsonObj);
                var stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                formDataContent.Add(stringContent, "formData");
                var _http = client.CreateClient("apiClient");
                /* token from cookie */
                var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
                _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", cookieToken);
                var response = _http.PostAsync("Post/AddPost", formDataContent).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("ShowPosts", "Post");
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Access");
                }
            }
        }

        [HttpGet]
        public IActionResult ShowPosts()
        {
            // IEnumerable<PostModel> catList;
            var _http = client.CreateClient("apiClient");
            var response = _http.GetAsync("Post/ShowPosts").Result;
            var catList = response.Content.ReadAsAsync<IEnumerable<PostModel>>().Result;
            return View(catList);
        }
        [HttpGet]
        public IActionResult GetPostByID(int id)
        {
            var _http = client.CreateClient("apiClient");
            var response = _http.GetAsync("Post/GetPostByID/" + id).Result;
            var list = response.Content.ReadAsAsync<PostModel>().Result;
            return View(list);
        }

        [HttpPost]
        public IActionResult UpdatePost(IFormFile imageUpload)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            imageUpload = Request.Form.Files.FirstOrDefault();
            var formViewData = Request.Form["dataObj"];
            var jsonObj = JsonConvert.DeserializeObject(formViewData);
            if (imageUpload != null)
            {
                byte[] bt;
                using (BinaryReader br = new BinaryReader(imageUpload.OpenReadStream()))
                {
                    bt = br.ReadBytes((int)imageUpload.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(bt);
                MultipartFormDataContent formData = new MultipartFormDataContent();
                var jsonObject = JsonConvert.SerializeObject(jsonObj);
                var stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                formData.Add(bytes, "img", imageUpload.FileName);
                formData.Add(stringContent, "formdata");
                var _http = client.CreateClient("apiClient");
                _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", cookieToken);
                var response = _http.PostAsync("Post/UpdatePost", formData).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("ShowPosts", "Post");
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Access");
                }
            }
            else
            {
                MultipartFormDataContent formDataContent = new MultipartFormDataContent();
                var jsonObject = JsonConvert.SerializeObject(jsonObj);
                var stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                formDataContent.Add(stringContent, "formData");
                var _http = client.CreateClient("apiClient");
                _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", cookieToken);
                var response = _http.PostAsync("Post/UpdatePost", formDataContent).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("ShowPosts", "Post");
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Access");
                }
            }
        }

        [HttpGet]
        public IActionResult DeletePost(int id)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.DeleteAsync("Post/DeletePost/" + id).Result;
            var postResponse = _http.DeleteAsync("Post/DeletePostFromPostCategoryModel/"+id).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("ShowPosts", "Post");
            }
            else
            {
                return RedirectToAction("PageNotFound", "Access");
            }
        }

        [HttpGet]
        public IActionResult CategoryForPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeletePostFromCategory(APostCategoryModel model)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.PostAsJsonAsync("Post/DeleteCategoryPost",model).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("ShowPosts", "Post");
            }
            else
            {
                return RedirectToAction("PageNotFound", "Access");
            }
        }
    }
}