using HackathonCCR.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace HackathonCCR.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly Services.IAuthenticationService _authenticationService;
        private readonly Services.IUserService _userService;

        public AuthenticationController(Services.IAuthenticationService authenticationService, Services.IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User != null && HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction(GetRedirectUrl(returnUrl));
            }

            return View("Login", null);
        }

        [HttpPost]
        public ActionResult Login(AuthenticationModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var user = _userService.GetUser(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Usuário não encontrado");
            }
            else
            {
                var confirmLogin = _authenticationService.ConfirmLogin(user, model);

                if (confirmLogin)
                {
                    var claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.Name, user.Name));
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = true,
                        IssuedUtc = DateTime.UtcNow,
                        RedirectUri = "/Home/Index"
                    };

                    var identity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity),
                        authProperties);

                    return RedirectToAction("Portal", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "Senha incorreta");
                }
            }

            return View(model);
        }

        public string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                return Url.Action("Portal", "Home");

            return returnUrl;
        }

        public ActionResult LogOff()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Authentication");
        }
    }
}
