using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleCore.Models;

namespace SampleCore.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async  Task<JsonResult> Validate(LoginModel login)
        {
            if (login.Email=="test@test.com" && login.Password=="test")
            {
                HttpContext.Session.SetString("userId", login.Email);

                var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, login.Email)
};

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return Json(new { status = true, message = "Login Successfull!" });
              
            }
            else
            {
                return Json(new { status = false, message = "Invalid Email!" });
            }
        }

    }
}