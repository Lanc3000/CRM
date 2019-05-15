using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using CRMCore.Services;
using CRMDeveloper.Models;
using Microsoft.Extensions.Logging;

namespace CRMDeveloper.Controllers
{
    public class LoginController : Base.BaseController
    {
        //private readonly ILogger<LoginController> _logger;

        private IUserService _userService { get; set; }
        public LoginController (IUserService userService
            /*ILogger<LoginController> logger*/)
        {
            _userService = userService;
            //_logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            //_logger.LogInformation("Index start");
            if(ModelState.IsValid)
            {
                var User = _userService.Login(loginViewModel.Email, loginViewModel.Password);
                if(User != null)
                {
                    //_logger.LogInformation("Auth user");
                    await Authenticate(User);
                    Response.Cookies.Append("Id", User.Id.ToString());
                    Response.Cookies.Append("Name", User.Fio?? "Ваше имя");
                    Response.Cookies.Append("Position", User.Position ?? "Должность");
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некоректные логин и (или) пароль");
            }
            return View();
        }
       

        private async Task Authenticate (CRMCore.Objects.ObjUser user)
        {
            string Permisions = "";
            for (int i = 0; i < user.Role.RoleActivitys.Count; i++)
            {
                Permisions += user.Role.RoleActivitys[i]+',';
            }
            //_logger.LogInformation("Вытащили активити");
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Title??"user"),
                new Claim("Permissions", Permisions)
            };
           
            ClaimsIdentity id = new ClaimsIdentity(claims, "CRMDeveloperCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            //_logger.LogInformation("Создали клайм и начали авторизацию");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}