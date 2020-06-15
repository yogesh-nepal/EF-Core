using System.Net.NetworkInformation;
using System.Threading;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using EFmodels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFcoreMVC.Controllers
{
    public class AccessController : Controller
    {
        private readonly IHttpClientFactory client;
        public AccessController(IHttpClientFactory _client)
        {
            this.client = _client;
        }
        [AllowAnonymous, HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.Url = returnUrl;
            return View();
        }
        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> Login(string UserEmailID, string UserPassword, UserModel model, string ReturnUrl = null)
        {
            var _http = client.CreateClient("apiClient");
            var response = _http.PostAsJsonAsync("User/UserLogin", model).GetAwaiter().GetResult();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var _token = response.Content.ReadAsAsync<string>().GetAwaiter().GetResult();
                HttpContext.Session.SetString("token", _token.ToString());

                /* Get Claims from JwtToken*/
                var token = new JwtSecurityTokenHandler().ReadJwtToken(_token);
                var claimUserName = token.Claims.FirstOrDefault(c => c.Type == "usrName").Value;
                var claimUserEmail = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

                /* Use Claims from token for cookie authentication */
                var claims = new List<Claim>
                {
                    new Claim("_token",_token.ToString()),
                    new Claim(ClaimTypes.Name,claimUserName),
                    new Claim(ClaimTypes.Email,claimUserEmail)
                };

                var claimRole = token.Claims.Where(c => c.Type == ClaimTypes.Role);
                foreach (var item in claimRole)
                {
                    var role = item.Value;
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrinciple = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrinciple);
                // Thread.CurrentPrincipal = claimsPrinciple;
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("ShowAll", "User");
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                ViewBag.Message = ("Username and password Incorrect");
                return View();
            }
            else
            {
                return RedirectToAction("ServerOffline", "Access");
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult PageNotFound()
        {
            return View();
        }
        public IActionResult ServerOffline()
        {
            return View();
        }

    }
}