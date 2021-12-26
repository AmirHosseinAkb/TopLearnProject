using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs.User;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Pages.Admin.Users
{
    public class DeletedUsersModel : PageModel
    {
        private IUserService _userService;
        public DeletedUsersModel(IUserService userService)
        {
            _userService = userService;
        }
        public GetUsersForShowInAdminViewModel GetUsersForShowInAdminViewModel { get; set; }
        public void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            GetUsersForShowInAdminViewModel=_userService.GetDeletedUsers(pageId, filterUserName, filterEmail);
        }
    }
}
