using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs.User;
using TopLearn.Data.Entities.Wallet;

namespace TopLearn.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class WalletController : Controller
    {
        private IUserService _userService;
        public WalletController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            ViewData["UserWallets"] = _userService.GetUserWalletsForShow(User.Identity.Name);
            return View();
        }

        [Route("ChargeWallet")]
        public IActionResult ChargeWallet(ChargeWalletViewModel charge)
        {
            if (charge.Amount < 5000)
            {
                ModelState.AddModelError("Amount", "مبلغ تراکنش حداقل باید 5000 تومان باشد");
                return View(charge);
            }

            int userId = _userService.GetUserIdByUserName(User.Identity.Name);
            Wallet wallet = new Wallet()
            {
                Amount = charge.Amount,
                Description = "شارژ کیف پول",
                CreateDate = DateTime.Now,
                IsPayed = false,
                TypeId = 1,
                UserId = userId
            };
            //AddWallet
            int walletId = _userService.AddWallet(wallet);

            var payment = new ZarinpalSandbox.Payment(charge.Amount);
            var response = payment.PaymentRequest("شارژ کیف پول", "http://localhost:28503/OnlinePayment/" + walletId);
            if (response.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + response.Result.Authority);
            }

            return Redirect("/UserPanel/Wallet");
        }
    }
}
