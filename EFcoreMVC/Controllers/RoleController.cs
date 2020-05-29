using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using EFmodels;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EFcoreMVC.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IHttpClientFactory client;
        public RoleController(IHttpClientFactory _client)
        {
            client = _client;
        }
        [HttpGet]
        public IActionResult GetAllRole()
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            IEnumerable<RoleModel> list;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.GetAsync("Role/ShowAll").Result;
            list = response.Content.ReadAsAsync<IEnumerable<RoleModel>>().Result;
            return Json(list);
        }

        [HttpGet]
        public IActionResult AddRoleForUser()
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
        public IActionResult InsertRoleForUser(UserWithRoleModel userWithRoleModel)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.PostAsJsonAsync("UserWithRole/Insert", userWithRoleModel).Result;
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return RedirectToAction("PageNotFound", "Access");
            }
            return RedirectToAction("ShowAll", "User");
        }

        [HttpPost]
        public IActionResult DeleteRoleForUser(int id)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.DeleteAsync("UserWithRole/Delete/" + id).Result;
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return RedirectToAction("PageNotFound", "Access");
            }
            return RedirectToAction("ShowAll", "User");
        }

        [HttpGet]
        public IActionResult AddRole()
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
        public IActionResult AddRole(RoleModel roleModel)
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.PostAsJsonAsync("Role/Insert", roleModel).GetAwaiter().GetResult();
            return RedirectToAction("ShowAll");
        }
        [HttpGet]
        public IActionResult ShowAll()
        {
            /* token from cookie */
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            IEnumerable<RoleModel> list;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.GetAsync("Role/ShowAll").Result;
            list = response.Content.ReadAsAsync<IEnumerable<RoleModel>>().Result;
            return View(list);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var cookieToken = HttpContext.User.Claims.First(c => c.Type == "_token").Value;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", cookieToken);
            var response = _http.DeleteAsync("Role/Delete/"+id).GetAwaiter().GetResult();
            return RedirectToAction("ShowAll");
        }
    }
}