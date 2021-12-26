using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Data.Entities.User;
using TopLearn.Core.DTOs.User;
using TopLearn.Data.Entities.Wallet;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
        #region Account
        bool IsExistUserByUserName(string username);
        bool IsExistUserByEmail(string email);
        int AddUser(User user);
        bool ActiveAccount(string activeCode);
        void UpdateUser(User user);
        User GetUserForLogin(string email, string password);
        User GetUserById(int userId);
        User GetUserByUserName(string username);
        int GetUserIdByUserName(string username);
        #endregion


        #region UserPanel
        UserInformationsViewModel GetUserInformationsForShow(string username);
        UserInformationsViewModel GetUserInformationsForShow(int userId);
        SideBarInformationsViewModel GetUserSideBarInformations(string username);
        EditUserProfileViewModel GetUserforEdit(string username);
        void EditUserProfile(string username,EditUserProfileViewModel edit);
        void ChangeUserPassword(string username, ChangeUserPasswordViewModel change);

        #endregion

        #region Wallet
        List<GetWalletsForShowViewModel> GetUserWalletsForShow(string username);
        int AddWallet(Wallet wallet);
        Wallet GetWalletByWalletId(int walletId);
        void UpdateWallet(Wallet wallet);
        int BalanceUserWallet(string username);
        #endregion


        #region Admin User
        GetUsersForShowInAdminViewModel GetUsers(int pageId = 1, string filterUserName = "", string filterEmail = "");
        GetUsersForShowInAdminViewModel GetDeletedUsers(int pageId = 1, string filterUserName = "", string filterEmail = "");
        int AddUserFromAdmin(CreateUserViewModel create);
        void EditUserFromAdmin(int userId, EditUserFromAdminViewModel edit);
        EditUserFromAdminViewModel GetUserForEditInAdmin(int userId);
        void DeleteUser(string userName);
        #endregion
    }
}
