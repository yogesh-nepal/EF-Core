using System.Data;
using System.Xml;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Net;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using EFmodels;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace EFcoreMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IHttpClientFactory client;
        public UserController(IHttpClientFactory _client)
        {
            client = _client;
        }


        // [HttpGet]
        // public IActionResult ShowAlll()
        // {
        //     var cookieRole = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role);
        //     /* token from cookie */
        //     var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
        //     foreach (var item in cookieRole)
        //     {
        //         var role = item.Value;
        //         if (role == "Admin")
        //         {
        //             IEnumerable<UserModel> userList;
        //             var _http = client.CreateClient("apiClient");
        //             _http.DefaultRequestHeaders.Authorization =
        //             new AuthenticationHeaderValue("Bearer", cookieToken);
        //             var response = _http.GetAsync("User/ShowAll").Result;
        //             if (response.StatusCode == HttpStatusCode.OK)
        //             {
        //                 userList = response.Content.ReadAsAsync<IEnumerable<UserModel>>().Result;
        //                 return View(userList);
        //             }
        //         }
        //     }
        //     return RedirectToAction("AccessDenied", "Access");
        // }

        [HttpGet]
        public IActionResult ShowAll(
            string sortOrder,
            string searchString,
            string currentFilter,
            int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["EmailSortParm"] = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            ViewData["RoleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "role" : "";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            var cookieRole = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role);
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            foreach (var item in cookieRole)
            {
                var role = item.Value;
                if (role == "Admin")
                {
                    IEnumerable<UserModel> userList;
                    var _http = client.CreateClient("apiClient");
                    _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", cookieToken);
                    var response = _http.GetAsync("User/ShowAll").Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        userList = response.Content.ReadAsAsync<IEnumerable<UserModel>>().Result;
                        if (searchString != null)
                        {
                            pageNumber = 1;
                        }
                        else
                        {
                            searchString = currentFilter;
                        }
                        if (!String.IsNullOrEmpty(searchString))
                        {
                            userList = userList.Where(s => s.UserFullName.Contains(searchString)
                                                   || s.UserEmailID.Contains(searchString));
                        }
                        switch (sortOrder)
                        {
                            case "name_desc":
                                userList = userList.OrderByDescending(s => s.UserFullName);
                                break;
                            case "role":
                                userList = userList.OrderBy(s => s.roles.RoleName);
                                break;
                            case "email_desc":
                                userList = userList.OrderByDescending(s => s.UserEmailID);
                                break;
                            default:
                                userList = userList.OrderBy(s => s.UserFullName);
                                break;
                        }
                        int pageSize = 10;
                        // return View(userList);
                        return View(PaginatedList<UserModel>.Create(userList, pageNumber ?? 1, pageSize));
                        // return View(userList);
                    }
                }
            }
            return RedirectToAction("AccessDenied", "Access");
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            var cookieRole = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role);
            foreach (var item in cookieRole)
            {
                var role = item.Value;
                if (role == "Admin")
                {
                    return View();
                }
            }
            return RedirectToAction("AccessDenied", "Access");
        }

        /* importing from external document and adding to UserModel */
        [HttpPost]
        public IActionResult FromCSV(IFormFile csvdt)
        {
            csvdt = Request.Form.Files.FirstOrDefault();
            if (csvdt != null)
            {
                byte[] bt;
                using (BinaryReader br = new BinaryReader(csvdt.OpenReadStream()))
                {
                    bt = br.ReadBytes((int)csvdt.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(bt);
                MultipartFormDataContent formDataContent = new MultipartFormDataContent();
                formDataContent.Add(bytes, "csv", csvdt.FileName);
                var _http = client.CreateClient("apiClient");
                /* token from cookie */
                var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
                _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", cookieToken);
                var response = _http.PostAsync("User/FromCSV", formDataContent).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("ShowAll");
                }
            }
            return RedirectToAction("AccessDenied", "Access");
        }


        [HttpPost]
        public IActionResult AddUser(UserModel model)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.PostAsJsonAsync("User/Insert", model).Result;
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ViewBag.Message = ("Email Already Registered!");
                return View();
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("ShowAll");
            }
            else
            {
                return RedirectToAction("AccessDenied", "Access");
            }
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            IEnumerable<UserModel> list;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.GetAsync("User/GetAllUsers").Result;
            list = response.Content.ReadAsAsync<IEnumerable<UserModel>>().Result;
            list = list.OrderBy(x => x.UserEmailID);
            return Json(list);
        }

        // [HttpGet]
        // public IActionResult ShowUsers()
        // {
        //     /* token from cookie */
        //     var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
        //     IEnumerable<UserModel> list;
        //     var _http = client.CreateClient("apiClient");
        //     _http.DefaultRequestHeaders.Authorization =
        //     new AuthenticationHeaderValue("Bearer", cookieToken);
        //     var response = _http.GetAsync("User/GetAllUsers").Result;
        //     list = response.Content.ReadAsAsync<IEnumerable<UserModel>>().Result;
        //     return View(list);
        // }

        /* For Pagination Practice*/
        [HttpGet]
        public IActionResult ShowUser(
            string sortOrder,
            string searchString,
            string currentFilter,
            int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["EmailSortParm"] = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            IEnumerable<UserModel> userList;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.GetAsync("User/GetAllUsers").Result;
            userList = response.Content.ReadAsAsync<IEnumerable<UserModel>>().Result;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                userList = userList.Where(s => s.UserFullName.Contains(searchString)
                                       || s.UserEmailID.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    userList = userList.OrderByDescending(s => s.UserFullName);
                    break;
                case "email_desc":
                    userList = userList.OrderByDescending(s => s.UserEmailID);
                    break;
                default:
                    userList = userList.OrderBy(s => s.UserFullName);
                    break;
            }
            int pageSize = 15;
            // return View(userList);
            return View(PaginatedList<UserModel>.Create(userList, pageNumber ?? 1, pageSize));
        }



        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.DeleteAsync("User/Delete/" + id).Result;
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "User");
            }
            return RedirectToAction("UserList");
        }
    }
}