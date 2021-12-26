using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Order;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Context;
using TopLearn.Data.Entities.Order;
using TopLearn.Data.Entities.User;
using TopLearn.Data.Entities.Wallet;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Core.Services
{
    public class OrderService : IOrderService
    {
        private TopLearnContext _context;
        private IUserService _userService;
        public OrderService(TopLearnContext context,IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public int AddOrder(string userName, int courseId)
        {
            var userId = _userService.GetUserIdByUserName(userName);
            var course = _context.Courses.SingleOrDefault(c => c.CourseId == courseId);

            var order = _context.Orders.SingleOrDefault(o => o.UserId == userId && !o.IsFinally);
            if (order == null)
            {
                order = new Order()
                {
                    UserId = userId,
                    IsFinally = false,
                    CreateDate = DateTime.Now,
                    OrderSum = course.CoursePrice,
                    OrderDetails =new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            CourseId=course.CourseId,
                            Count=1,
                            Price=course.CoursePrice
                        }
                    }
                };
                _context.Orders.Add(order);
            }
            else
            {
                var orderDetail = _context.OrderDetails.SingleOrDefault(d => d.OrderId == order.OrderId && d.CourseId == course.CourseId);
                if (orderDetail != null)
                {
                    orderDetail.Count += 1;
                    _context.OrderDetails.Update(orderDetail);
                }
                else
                {
                    orderDetail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        Count = 1,
                        Price = course.CoursePrice,
                        CourseId = course.CourseId,
                    };
                    _context.OrderDetails.Add(orderDetail);
                    order.OrderSum += course.CoursePrice;
                }
            }
            _context.SaveChanges();
            return order.OrderId;
        }

        public Order GetOrderById(string userName,int orderId)
        {
            var userId = _userService.GetUserIdByUserName(userName);
            return _context.Orders.Include(o => o.OrderDetails).ThenInclude(d=>d.Course)
                .SingleOrDefault(o => o.OrderId == orderId && o.UserId==userId);
        }

        public List<Order> GetUserOrders(string userName)
        {
            var userId = _userService.GetUserIdByUserName(userName);
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        public bool FinalOrder(string userName, int orderId)
        {
            var userId = _userService.GetUserIdByUserName(userName);
            var order = _context.Orders.Include(o => o.OrderDetails)
                .SingleOrDefault(o => o.OrderId == orderId && o.UserId == userId);

            if(order==null || order.IsFinally)
            {
                return false;
            }
            if (_userService.BalanceUserWallet(userName) > order.OrderSum)
            {
                order.IsFinally = true;
                Wallet wallet = new Wallet()
                {
                    UserId=userId,
                    Description="پرداخت فاکتور شماره#"+order.OrderId.ToString(),
                    CreateDate=DateTime.Now,
                    Amount=order.OrderSum,
                    IsPayed=true,
                    TypeId=2
                };
                foreach (var detail in order.OrderDetails)
                {
                    _context.UserCourses.Add(new UserCourse()
                    {
                        UserId=order.UserId,
                        CourseId=detail.CourseId
                    });
                }
                _context.Orders.Update(order);
                _userService.AddWallet(wallet);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public DiscountUseType UseDiscount(int orderId, string code)
        {
            var discount = _context.Discounts.SingleOrDefault(d => d.DiscountCode == code);
            var order = _context.Orders.SingleOrDefault(o => o.OrderId == orderId);

            if (discount == null)
                return DiscountUseType.NotFound;

            if (discount.StartDate > DateTime.Now)
                return DiscountUseType.Expired;

            if (discount.EndDate < DateTime.Now)
                return DiscountUseType.Expired;

            if (discount.UsableCount != null && discount.UsableCount < 1)
                return DiscountUseType.Finished;

            if (_context.UserDiscounts.Any(ud => ud.UserId ==order.UserId  && ud.DiscountId == discount.DiscountId))
                return DiscountUseType.UsedByUser;
            

            order.OrderSum -= (order.OrderSum * discount.DiscountPercent)/100;
            UpdateOrder(order);
            if (discount.UsableCount != null)
            {
                discount.UsableCount -= 1;
                _context.Discounts.Update(discount);
            }
            UserDiscount userDiscount = new UserDiscount()
            {
                UserId = order.UserId,
                DiscountId = discount.DiscountId
            };
            _context.UserDiscounts.Add(userDiscount);
            _context.SaveChanges();
            return DiscountUseType.Success;
        }

        public void AddDiscount(Discount discount)
        {
            _context.Discounts.Add(discount);
            _context.SaveChanges();
        }

        public List<Discount> GetAllDiscounts()
        {
            return _context.Discounts.ToList();
        }

        public Discount GetDiscountById(int discountId)
        {
            return _context.Discounts.SingleOrDefault(d => d.DiscountId == discountId);
        }

        public void EditDiscount(Discount discount)
        {
            _context.Discounts.Update(discount);
            _context.SaveChanges();
        }

        public bool IsExistDiscount(string code)
        {
            return _context.Discounts.Any(d => d.DiscountCode == code);
        }

        public Discount GetDiscountByIdNoTracking(int discountId)
        {
            return _context.Discounts.AsNoTracking().SingleOrDefault(d => d.DiscountId == discountId);
        }

        public bool IsUserHaveCourse(string userName, int courseId)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            return _context.UserCourses.Any(uc => uc.UserId == userId && uc.CourseId == courseId);
        }
    }
}
