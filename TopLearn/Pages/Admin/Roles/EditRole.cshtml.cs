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
    public class EditRoleModel : PageModel
    {
        private IPermissionService _permissionService;
        public EditRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [BindProperty]
        public Role Role { get; set; }
        public void OnGet(int roleId)
        {
            Role = _permissionService.GetRoleById(roleId);
            ViewData["Permissions"] = _permissionService.GetAllPermissions();
            ViewData["RolePermissions"] = _permissionService.GetRolePermissions(roleId);
        }

        public IActionResult OnPost(List<int> SelectedPermissions)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //EditRole
            _permissionService.UpdateRole(Role);
            //Edit Role Permissions
            _permissionService.EditRolePermissions(Role.RoleId, SelectedPermissions);
            return RedirectToPage("Index");
        }
    }
}
