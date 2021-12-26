using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs.User;

namespace TopLearn.Pages.Admin.Users
{
    public class EditUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;
        public EditUserModel(IUserService userService,IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }
        [BindProperty]
        public EditUserFromAdminViewModel EditUserFromAdminViewModel { get; set; }
        public void OnGet(int userId)
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
            ViewData["UserRoles"] = _permissionService.GetUserRoles(userId);
            EditUserFromAdminViewModel = _userService.GetUserForEditInAdmin(userId);
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Edit User
            _userService.EditUserFromAdmin(EditUserFromAdminViewModel.UserId, EditUserFromAdminViewModel);

            //EditUserRoles
            _permissionService.EditUserRoles(EditUserFromAdminViewModel.UserId, SelectedRoles);
            return RedirectToPage("Index");
        }
    }
}
