<<<<<<< HEAD
using System.Net.NetworkInformation;
=======
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
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
<<<<<<< HEAD
        
=======
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953

        [HttpGet]
        public IActionResult ShowAll()
        {
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

                    // var header= new AuthenticationHeaderValue("Bearer",HttpContext.Session.GetString("token"));
                    // _http.DefaultRequestHeaders.Authorization = header;

                    _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", cookieToken);

                    // var request= new HttpRequestMessage(HttpMethod.Get,_http.BaseAddress.AbsoluteUri+"User/ShowAll");
                    // request.Headers.Add("Authorization","Bearer "+HttpContext.Session.GetString("token"));

                    var response = _http.GetAsync("User/ShowAll").Result;
                    // var response = _http.SendAsync(request).GetAwaiter().GetResult();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        userList = response.Content.ReadAsAsync<IEnumerable<UserModel>>().Result;
                        return View(userList);
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

        [HttpPost]
        public IActionResult AddUser(UserModel model)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.PostAsJsonAsync("User/Insert", model).Result;
            return RedirectToAction("ShowAll");
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            IEnumerable<UserModel> list;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",cookieToken);
            var response = _http.GetAsync("User/GetAllUsers").Result;
            list = response.Content.ReadAsAsync<IEnumerable<UserModel>>().Result;
            return Json(list);
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