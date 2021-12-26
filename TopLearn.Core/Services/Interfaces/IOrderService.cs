using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Data.Entities.Order;
using TopLearn.Core.DTOs.Order;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IOrderService
    {
        int AddOrder(string userName, int courseId);
        Order GetOrderById(string userName, int orderId);
        List<Order> GetUserOrders(string userName);
        void UpdateOrder(Order order);
        DiscountUseType UseDiscount(int orderId, string code);
        bool FinalOrder(string userName, int orderId);
        void AddDiscount(Discount discount);
        List<Discount> GetAllDiscounts();
        Discount GetDiscountById(int discountId);
        Discount GetDiscountByIdNoTracking(int discountId);
        void EditDiscount(Discount discount);
        bool IsExistDiscount(string code);
        bool IsUserHaveCourse(string userName, int courseId);
    }
}
