using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs.User;
using TopLearn.Data.Entities.User;
using TopLearn.Core.Convertors;
using TopLearn.Core.Generators;

namespace TopLearn.Pages.Admin.Users
{
    public class CreateUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;
        public CreateUserModel(IUserService userService,IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }
        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _permissionService.GetAllRoles();
                return Page();
            }
            if (CreateUserViewModel.UserName.Length < 5)
            {
                ModelState.AddModelError("UserName", "نام کاربری باید حداقل متشکل از 5 کاراکتر باشد");
                return Page();
            }
            if (CreateUserViewModel.Password.Length < 8)
            {
                ModelState.AddModelError("Password", "رمز عبور باید حداقل متشکل از 8 کاراکتر باشد");
                return Page();
            }

            //Add User
            int userId = _userService.AddUserFromAdmin(CreateUserViewModel);
            //Add User Roles
            _permissionService.AddUserRoles(userId, SelectedRoles);
            return RedirectToPage("Index");
        }
    }
}
