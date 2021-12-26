using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;


namespace TopLearn.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class MyOrdersController : Controller
    {
        private IOrderService _orderService;
        public MyOrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View(_orderService.GetUserOrders(User.Identity.Name));
        }
        [Route("ShowOrder/{orderId}")]
        public IActionResult ShowOrder(int orderId,string type,bool isFinaled=false)
        {
            var order = _orderService.GetOrderById(User.Identity.Name, orderId);
            if (order == null)
            {
                return BadRequest();
            }
            ViewBag.Type = type;
            ViewBag.isFinaled = isFinaled;
            return View(order);
        }
        [Route("UseDiscount")]
        public IActionResult UseDiscount(int orderId,string code)
        {
            var type = _orderService.UseDiscount(orderId, code);
            return Redirect("/ShowOrder/" + orderId + "?type=" + type);
        }
        
        [Route("FinalOrder/{orderId}")]
        public IActionResult FinalOrder(int orderId)
        {
            if (_orderService.FinalOrder(User.Identity.Name, orderId))
            {
                return Redirect("/ShowOrder/"+orderId+"?isFinaled=true");
            }
            return BadRequest();
        }
    }
}
