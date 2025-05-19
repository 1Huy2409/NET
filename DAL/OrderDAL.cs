using QLBS.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace QLBS.DAL
{
    public class OrderDAL
    {
        private readonly QLBSDbContext _context;

        public OrderDAL()
        {
            _context = new QLBSDbContext();
        }

        public bool AddOrderWithDetails(Order order, List<OrderDetail> orderDetails)
        {
            // Kiểm tra user tồn tại
            if (!_context.Users.Any(u => u.ID == order.UserId))
            {
                return false;
            }

            // Thêm order
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Thêm order details và cập nhật stock
            foreach (var detail in orderDetails)
            {
                var book = _context.Books.Find(detail.BookId);
                if (book == null || detail.quantity > book.Stock)
                {
                    return false;
                }

                // Cập nhật số lượng tồn kho
                book.Stock -= detail.quantity;
                detail.OrderId = order.ID;
                _context.OrderDetails.Add(detail);
            }

            _context.SaveChanges();
            return true;
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            return _context.Orders
                .Include(o => o.User)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public List<Order> GetOrdersByUserName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return GetAllOrders();
            }

            return _context.Orders
                .Include(o => o.User)
                .Where(o => o.User.Name.ToLower().Contains(name.ToLower()))
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }
    }
}