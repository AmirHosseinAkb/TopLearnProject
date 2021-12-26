using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Convertors;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Context;
using TopLearn.Data.Entities.User;
using TopLearn.Core.Generators;
using TopLearn.Core.Security;
using TopLearn.Core.DTOs.User;
using System.IO;
using TopLearn.Data.Entities.Wallet;
using Microsoft.EntityFrameworkCore;

namespace TopLearn.Core.Services
{
    public class UserService : IUserService
    {
        private TopLearnContext _context;
        public UserService(TopLearnContext context)
        {
            _context = context;
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public bool ActiveAccount(string activeCode)
        {
            var user= _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null)
            {
                return false;
            }
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            user.IsActive = true;
            UpdateUser(user);
            return true;
        }

        public bool IsExistUserByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == EmailConvertor.FixEmail(email));
        }

        public bool IsExistUserByUserName(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public User GetUserForLogin(string email, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Email == EmailConvertor.FixEmail(email) 
            && u.Password == PasswordHasher.HashPasswordMD5(password));
        }

        public UserInformationsViewModel GetUserInformationsForShow(string username)
        {
            var user = GetUserByUserName(username);

            UserInformationsViewModel information = new UserInformationsViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                RegiaterDate = user.RegisterDate,
                Wallet = BalanceUserWallet(username)
            };
            return information;
        }

        public SideBarInformationsViewModel GetUserSideBarInformations(string username)
        {
            var user = GetUserByUserName(username);
            SideBarInformationsViewModel sideBar = new SideBarInformationsViewModel()
            {
                UserName = user.UserName,
                AvatarName = user.AvatarName,
                RegisterDate = user.RegisterDate
            };
            return sideBar;
        }

        public User GetUserByUserName(string username)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == username);
        }

        public int GetUserIdByUserName(string username)
        {
            return GetUserByUserName(username).UserId;
        }

        public EditUserProfileViewModel GetUserforEdit(string username)
        {
            return _context.Users.Where(u => u.UserName == username)
                .Select(u => new EditUserProfileViewModel()
                {
                    UserName = u.UserName,
                    Email = u.Email,
                    AvatarName = u.AvatarName
                }).Single();
        }

        public void EditUserProfile(string username,EditUserProfileViewModel edit)
        {
            var user = GetUserByUserName(username);
            if (user.UserName!=edit.UserName)
            {
                user.UserName = edit.UserName;
            }
            if (user.Email!=EmailConvertor.FixEmail(edit.Email))
            {
                user.Email = EmailConvertor.FixEmail(edit.Email);
            }
            if (edit.UserAvatar != null)
            {
                string imagePath = "";
                if (edit.AvatarName != "Default.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "UserAvatar",
                        edit.AvatarName
                        );
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                user.AvatarName = NameGenerator.GenerateUniqCode() + Path.GetExtension(edit.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "UserAvatar",
                    user.AvatarName
                    );
                using(var stream=new FileStream(imagePath, FileMode.Create))
                {
                    edit.UserAvatar.CopyTo(stream);
                }
            }
            UpdateUser(user);
        }

        public void ChangeUserPassword(string username, ChangeUserPasswordViewModel change)
        {
            var user = GetUserByUserName(username);
            user.Password = PasswordHasher.HashPasswordMD5(change.NewPassword);
            UpdateUser(user);
        }

        public List<GetWalletsForShowViewModel> GetUserWalletsForShow(string username)
        {
            int userId = GetUserIdByUserName(username);
            return _context.Wallets.Where(w => w.UserId == userId)
                .Select(w => new GetWalletsForShowViewModel()
                {
                    Amount = w.Amount,
                    TypeId = w.TypeId,
                    CreateDate = w.CreateDate,
                    Description = w.Description,
                    IsPayed = w.IsPayed
                }).ToList();
        }

        public int AddWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            _context.SaveChanges();
            return wallet.WalletId;
        }

        public Wallet GetWalletByWalletId(int walletId)
        {
            return _context.Wallets.SingleOrDefault(w => w.WalletId == walletId);
        }

        public void UpdateWallet(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            _context.SaveChanges();
        }

        public int BalanceUserWallet(string username)
        {
            var userId = GetUserIdByUserName(username);

            var deposite = _context.Wallets.Where(w => w.UserId == userId&& w.TypeId==1 && w.IsPayed).ToList();
            var withdraw = _context.Wallets.Where(w => w.UserId == userId && w.TypeId == 2 && w.IsPayed).ToList();

            return deposite.Sum(w => w.Amount) - withdraw.Sum(w => w.Amount);
        }

        public GetUsersForShowInAdminViewModel GetUsers(int pageId=1,string filterUserName="",string filterEmail="")
        {
            IQueryable<User> result = _context.Users;

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }
            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            GetUsersForShowInAdminViewModel getUsers = new GetUsersForShowInAdminViewModel()
            {
                Users = result.Skip(skip).Take(take).ToList(),
                CurrentPage = pageId,
                PageCount = result.Count() / take
            };
            return getUsers;
        }

        public int AddUserFromAdmin(CreateUserViewModel create)
        {
            User user = new User()
            {
                UserName = create.UserName,
                Email = EmailConvertor.FixEmail(create.Email),
                Password = PasswordHasher.HashPasswordMD5(create.Password),
                RegisterDate = DateTime.Now,
                ActiveCode = NameGenerator.GenerateUniqCode(),
                IsActive = true,
                IsDeleted = false,
                AvatarName = "Default.png"
            };

            if (create.UserAvatar != null)
            {
                user.AvatarName = NameGenerator.GenerateUniqCode() + Path.GetExtension(create.UserAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "UserAvatar",
                    user.AvatarName
                    );
                using(var stream=new FileStream(imagePath, FileMode.Create))
                {
                    create.UserAvatar.CopyTo(stream);
                }
            }
            UpdateUser(user);
            return user.UserId;
        }

        public void EditUserFromAdmin(int userId, EditUserFromAdminViewModel edit)
        {
            var user = GetUserById(userId);
            user.Email = EmailConvertor.FixEmail(edit.Email);
            if (edit.Password != null)
            {
                user.Password = PasswordHasher.HashPasswordMD5(edit.Password);
            }
            if (edit.UserAvatar != null)
            {
                string imagePath = "";
                if (edit.AvatarName != "Default.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "UserAvatar",
                        edit.AvatarName
                        );
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                user.AvatarName = NameGenerator.GenerateUniqCode() + Path.GetExtension(edit.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "UserAvatar",
                        user.AvatarName
                        );

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    edit.UserAvatar.CopyTo(stream);
                }
            }
            UpdateUser(user);
        }

       
        public EditUserFromAdminViewModel GetUserForEditInAdmin(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId)
                .Select(u => new EditUserFromAdminViewModel()
                {
                    UserName = u.UserName,
                    Email = u.Email,
                    AvatarName = u.AvatarName,
                    UserId = u.UserId
                }).Single();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.UserId == userId);
        }

        public UserInformationsViewModel GetUserInformationsForShow(int userId)
        {
            var user = GetUserById(userId);

            UserInformationsViewModel information = new UserInformationsViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                RegiaterDate = user.RegisterDate,
                Wallet = BalanceUserWallet(user.UserName)
            };
            return information;
        }

        public void DeleteUser(string userName)
        {
            var user = GetUserByUserName(userName);
            user.IsDeleted = true;
            UpdateUser(user);
        }

        public GetUsersForShowInAdminViewModel GetDeletedUsers(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            IQueryable<User> result = _context.Users.IgnoreQueryFilters().Where(u=>u.IsDeleted);

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }
            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            GetUsersForShowInAdminViewModel getUsers = new GetUsersForShowInAdminViewModel()
            {
                Users = result.Skip(skip).Take(take).ToList(),
                CurrentPage = pageId,
                PageCount = result.Count() / take
            };
            return getUsers;
        }
    }
}
