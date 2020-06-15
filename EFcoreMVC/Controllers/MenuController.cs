using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using EFmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EFcoreMVC.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly IHttpClientFactory client;
        public MenuController(IHttpClientFactory _client)
        {
            client = _client;
        }
        [HttpGet]
        public IActionResult AddMenu()
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
        public IActionResult AddMenu(MenuModel menuModel)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.PostAsJsonAsync("Menu/Insert", menuModel).GetAwaiter().GetResult();
            return RedirectToAction("ShowAll");
        }

        [HttpGet]
        public IActionResult ShowAll()
        {
            IEnumerable<MenuModel> menuList;
            var _http = client.CreateClient("apiClient");
            var response = _http.GetAsync("Menu/ShowAll").Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                menuList = response.Content.ReadAsAsync<IEnumerable<MenuModel>>().Result;
                return View(menuList);
            }
            return RedirectToAction("AccessDenied", "Access");
        }
        [HttpGet]
        public IActionResult DeleteMenu(int id)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.DeleteAsync("Menu/Delete/" + id).GetAwaiter().GetResult();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("ShowAll");
            }
            return RedirectToAction("AccessDenied", "Access");
        }

        [HttpGet]
        public IActionResult GetMenuDetailsByID(int id)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.GetAsync("Menu/GetDetails/" + id).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var list = response.Content.ReadAsAsync<MenuModel>().Result;
                return View(list);
            }
            return RedirectToAction("PageNotFound", "Access");
        }
        [HttpPost]
        public IActionResult UpdateMenu(MenuModel menuModel)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.PostAsJsonAsync("Menu/Update", menuModel).GetAwaiter().GetResult();
            return RedirectToAction("ShowAll");
        }

        /* Add Menu For User */
        [HttpGet]
        public IActionResult AddMenuForUser()
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
        public IActionResult AddMenuForUser(UserMenuModel userMenuModel)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.PostAsJsonAsync("UserMenu/Insert", userMenuModel).GetAwaiter().GetResult();
            return RedirectToAction("ShowAll");
        }

        [HttpGet]
        public IActionResult LoadMenu()
        {
            IEnumerable<MenuModel> menuList;
            var _http = client.CreateClient("apiClient");
            var response = _http.GetAsync("Menu/ShowAll").Result;
            menuList = response.Content.ReadAsAsync<IEnumerable<MenuModel>>().Result;
            return Json(menuList);
        }

        [HttpGet]
        public IActionResult ShowUserMenu()
        {
            IEnumerable<UserModel> menuList;
            var _http = client.CreateClient("apiClient");
            var response = _http.GetAsync("UserMenu/ShowUserMenu").Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                menuList = response.Content.ReadAsAsync<IEnumerable<UserModel>>().Result;
                return View(menuList);
            }
            return RedirectToAction("AccessDenied", "Access");
        }
        [HttpGet]
        public IActionResult DeleteMenuForUser(int id)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.DeleteAsync("UserMenu/Delete/" + id).GetAwaiter().GetResult();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("ShowAll");
            }
            return RedirectToAction("AccessDenied", "Access");
        }
    }
}