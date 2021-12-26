using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs.User;
using TopLearn.Core.Convertors;
using TopLearn.Core.Security;

namespace TopLearn.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class HomeController : Controller
    {
        private IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View(_userService.GetUserInformationsForShow(User.Identity.Name));
        }

        [Route("UserPanel/EditUserProfile")]
        public IActionResult EditUserProfile()
        {
            var user = _userService.GetUserforEdit(User.Identity.Name);
            return View(user);
        }
        [Route("UserPanel/EditUserProfile")]
        [HttpPost]
        public IActionResult EditUserProfile(EditUserProfileViewModel edit)
        {
            if (!ModelState.IsValid)
            {
                return View(edit);
            }
            var user = _userService.GetUserByUserName(User.Identity.Name);
            if (edit.UserName.Length < 4)
            {
                ModelState.AddModelError("UserName", "نام کاربری نمیتواند کمتر از 4 کاراکتر باشد");
                return View(edit);
            }
            if (edit.UserName != user.UserName)
            {
                if (_userService.IsExistUserByUserName(edit.UserName))
                {
                    ModelState.AddModelError("UserName", "این نام کاربری از قبل وجود دارد");
                    return View(edit);
                }
            }
            if (EmailConvertor.FixEmail(edit.Email) !=user.Email )
            {
                if (_userService.IsExistUserByEmail(edit.Email))
                {
                    ModelState.AddModelError("Email", "این ایمیل از قبل وجود دارد");
                    return View(edit);
                }
            }
            //EditUserProfile
            _userService.EditUserProfile(User.Identity.Name, edit);

            return Redirect("/Login?editProfile=true");
        }

        [Route("UserPanel/ChangeUserPassword")]
        public IActionResult ChangeUserPassword()
        {
            return View();
        }

        [Route("UserPanel/ChangeUserPassword")]
        [HttpPost]
        public IActionResult ChangeUserPassword(ChangeUserPasswordViewModel change)
        {
            if (!ModelState.IsValid)
            {
                return View(change);
            }
            var user = _userService.GetUserByUserName(User.Identity.Name);
            if (user.Password != PasswordHasher.HashPasswordMD5(change.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی را بصورت صحیح وارد کنید");
                return View(change);
            }
            if (change.NewPassword.Length < 8)
            {
                ModelState.AddModelError("NewPassword", "کلمه عبور حداقل باید متشکل از 8 کاراکتر باشد");
                return View(change);
            }
            if (change.NewPassword != change.RepeatNewPassword)
            {
                ModelState.AddModelError("RepeatNewPassword", "تکرار کلمه عبور جدید را بصورت صحیح وارد کنید");
                return View(change);
            }

            //TODO:Change Password
            _userService.ChangeUserPassword(User.Identity.Name,change);

            ViewBag.IsChanged = true;
            return View();
        }
    }
}
