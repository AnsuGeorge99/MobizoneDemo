using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UILayer.Datas.Apiservices;
using Microsoft.AspNetCore.Authorization;
using DomainLayer;
using UILayer.ApiServices;

namespace UILayer.Controllers
{
    public class UserController : Controller
    {
        private UserApi _userApi;
        private IConfiguration _configuration;
        private Registration _registration;
        public UserController(IConfiguration configuration)
        {
            _userApi = new UserApi(_configuration);
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserRegister()
        
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserRegister(Registration registrationView)
        //public IActionResult UserRegister(Registration registration)
        {
            _userApi.UserRegister(registrationView);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            ViewData["LoginUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login loginView , string ReturnUrl)
        //public async Task<IActionResult> Login(Login loginView)
        {
            AdminApi admin = new AdminApi();
            if (ReturnUrl == "/admin")
            {
                if (admin.AdminLogin(loginView))
                {
                    var claims = new List<Claim>();

                    claims.Add(new Claim("password", loginView.password));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, loginView.username));
                    claims.Add(new Claim(ClaimTypes.Name, loginView.username));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    return Redirect("/admin/Index");
                }
            }
            Login userLogin = new Login();
            //Login userLogin = new Login();
            _registration = _userApi.GetUserInfo().Where(register => register.email == loginView.username).FirstOrDefault();
            userLogin = loginView;
            bool check = _userApi.UserLogin(loginView);
            if (check)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim("password", loginView.password));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, loginView.username));
                claims.Add(new Claim(ClaimTypes.Name, loginView.username));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("/user/Index");
            }
            TempData["Error"] = "*Invalid Email or Password";
            return View("Login");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgetPassword()
        {
            return View();
        }
    }
}
