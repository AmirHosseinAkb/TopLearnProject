using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.User;

namespace TopLearn.Pages.Admin.Roles
{
    public class CreateRoleModel : PageModel
    {
        private IPermissionService _permissionService;
        public CreateRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [BindProperty]
        public Role Role { get; set; }
        public void OnGet()
        {
            ViewData["Permissions"] = _permissionService.GetAllPermissions();
        }

        public IActionResult OnPost(List<int> SelectedPermissions)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Permissions"] = _permissionService.GetAllPermissions();
                return Page();
            }
            //Add Role
            int roleId = _permissionService.AddRole(Role);
            //Add Role Permissions
            _permissionService.AddRolePermissions(roleId, SelectedPermissions);
            return RedirectToPage("Index");
        }
    }
}
