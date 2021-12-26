using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Data.Entities.User
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "فرمت {0} صحیح نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تصویر کاربر")]
        public string AvatarName { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public DateTime RegisterDate { get; set; }

        public bool IsDeleted { get; set; }
        [Display(Name ="فعال است؟")]
        public bool IsActive { get; set; }

        [Display(Name = "کد فعالسازی")]
        public string ActiveCode { get; set; }


        #region Relations
        public List<UserRole> UserRoles { get; set; }
        public List<Wallet.Wallet> Wallets { get; set; }
        public List<Course.Course> Courses { get; set; }
        public List<Order.Order> Orders { get; set; }
        public List<UserDiscount> UserDiscounts { get; set; }
        public List<Course.UserCourse> UserCourses { get; set; }
        public List<Course.CourseComment> CourseComments { get; set; }
        #endregion
    }
}
