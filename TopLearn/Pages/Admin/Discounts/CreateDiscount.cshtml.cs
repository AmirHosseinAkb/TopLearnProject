using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Order;

namespace TopLearn.Pages.Admin.Discounts
{
    public class CreateDiscountModel : PageModel
    {
        private IOrderService _orderService; 
        public CreateDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public Discount Discount { get; set; }
        public void OnGet()
        {
            
        }

        public IActionResult OnPost(string stDate="", string edDate="")
        {
            if (stDate != null)
            {
                string[] std = stDate.Split("/");
                Discount.StartDate = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2])
                    , new PersianCalendar());
            }
            if (edDate != null)
            {
                string[] edt = edDate.Split("/");
                Discount.EndDate = new DateTime(int.Parse(edt[0]),
                    int.Parse(edt[1]),
                    int.Parse(edt[2])
                    , new PersianCalendar());
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_orderService.IsExistDiscount(Discount.DiscountCode))
            {
                ViewData["IsExistCode"] = true;
                return Page();
            }
            _orderService.AddDiscount(Discount);
            return RedirectToPage("Index");
        }
    }
}
