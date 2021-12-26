using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.User;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.Convertors;
using TopLearn.Data.Entities.User;
using TopLearn.Core.Security;
using TopLearn.Core.Generators;
using TopLearn.Core.Senders;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace TopLearn.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IViewRenderService _viewRender;
        public AccountController(IUserService userService, IViewRenderService viewRender)
        {
            _userService = userService;
            _viewRender = viewRender;
        }

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterUserViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            else if (_userService.IsExistUserByUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "این نام کاربری از قبل وجود دارد");
                return View(register);
            }
            else if (_userService.IsExistUserByEmail(register.Email))
            {
                ModelState.AddModelError("Email", "این ایمیل از قبل وجود دارد");
                return View(register);
            }

            User user = new User()
            {
                UserName = register.UserName,
                Email = EmailConvertor.FixEmail(register.Email),
                Password = PasswordHasher.HashPasswordMD5(register.Password),
                IsActive = false,
                ActiveCode = NameGenerator.GenerateUniqCode(),
                AvatarName = "Default.png",
                IsDeleted = false,
                RegisterDate = DateTime.Now
            };

            //Add User 
            int userId = _userService.AddUser(user);
            //SendEmail
            string body = _viewRender.RenderToStringAsync("_SendRegisterEmailView", user);
            SendEmail.Send(user.Email, "تاپلرن|فعالسازی حساب کاربری", body);
            
            return View("_SuccessRegister",user);
        }

        [Route("ActiveAccount/{activeCode}")]
        public IActionResult ActiveAccount(string activeCode)
        {
            ViewBag.IsActive = _userService.ActiveAccount(activeCode);
            return View();
        }
        
        [Route("/Login")]

        public IActionResult Login(bool editProfile=false)
        {
            ViewBag.isEdited = editProfile;
            return View();
        }

        [Route("/Login")]
        [HttpPost]
        public IActionResult Login(LoginUserViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userService.GetUserForLogin(login.Email, login.Password);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };

                    var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    var principle = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberMe
                    };

                    HttpContext.SignInAsync(principle, properties);

                    ViewBag.IsLogedIn = true;

                    return View();
                }
                else
                {
                    ModelState.AddModelError("Password", "حساب کاربری شما فعال نیست");
                    return View(login);
                }
            }
            else
            {
                ModelState.AddModelError("Password", "کاربری با این مشخصات پیدا نشد");
                return View(login);
            }
        }

        [Route("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
    }
}
