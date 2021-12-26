using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Data.Entities.Permission
{
    public class RolePermission
    {
        [Key]
        public int RP_Id { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int PermissionId { get; set; }

        #region Realtions
        public User.Role Role { get; set; }
        public Permission Permission { get; set; }

        #endregion
    }
}
