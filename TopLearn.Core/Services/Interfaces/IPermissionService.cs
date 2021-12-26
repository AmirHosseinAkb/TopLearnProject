using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Data.Entities.User;
using TopLearn.Data.Entities.Permission;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        List<Role> GetAllRoles();
        void AddUserRoles(int userId, List<int> roleIds);
        List<int> GetUserRoles(int userId);
        void EditUserRoles(int userId, List<int> roleIds);
        List<Permission> GetAllPermissions();
        int AddRole(Role role);
        void AddRolePermissions(int roleId, List<int> permissionIds);
        Role GetRoleById(int roleId);
        List<int> GetRolePermissions(int roleId);
        void UpdateRole(Role Role);
        void EditRolePermissions(int roleId, List<int> permissionIds);
        void DeleteRole(Role role);
        bool IsUserHavePermission(string username, int permissionId);
    }
}
