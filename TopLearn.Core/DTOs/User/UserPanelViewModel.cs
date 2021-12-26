using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs.User
{
    public class UserInformationsViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime RegiaterDate { get; set; }
        public int Wallet { get; set; }
    }
    public class SideBarInformationsViewModel
    {
        public string UserName { get; set; }
        public string AvatarName { get; set; }
        public DateTime RegisterDate { get; set; }
    }

    public class EditUserProfileViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "فرمت {0} صحیح نمی باشد")]
        public string Email { get; set; }
        public string AvatarName { get; set; }
        public IFormFile UserAvatar { get; set; }
    }

    public class ChangeUserPasswordViewModel
    {
        [Display(Name = "رمز عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "تکرار کلمه عبور جدید را بصورت صحیح وارد کنید")]
        public string RepeatNewPassword { get; set; }
    }

    
}
