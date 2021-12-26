using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;
        private ICourseService _courseService;
        public HomeController(IUserService userService,ICourseService courseService)
        {
            _userService = userService;
            _courseService = courseService;
        }
        public IActionResult Index()
        {
            ViewBag.PapularCourses = _courseService.GetPopularCourses();
            return View(_courseService.GetCourse());
        }
        [Route("OnlinePayment/{walletId}")]
        public IActionResult OnlinePayment(int walletId)
        {
            if (HttpContext.Request.Query["Status"] != ""
                && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                var authority = HttpContext.Request.Query["Authority"];

                var wallet = _userService.GetWalletByWalletId(walletId);

                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var response = payment.Verification(authority).Result;
                if (response.Status == 100)
                {
                    wallet.IsPayed = true;
                    _userService.UpdateWallet(wallet);
                    ViewBag.Code = response.RefId;
                    ViewBag.IsPayed = true;
                }
            }
            return View();
        }
        [Route("/Home/GetSubGroups/{groupId}")]
        public IActionResult GetSubGroups(int groupId)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text="لطفا انتخاب کنید",Value=""}
            };
            var subGroups = _courseService.GetSubGroupsOfGroup(groupId);
            list.AddRange(subGroups);

            return Json(new SelectList(list, "Value", "Text"));
        }

        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/MyImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/MyImages/"}{fileName}";


            return Json(new { uploaded = true, url });
        }
    }
}
