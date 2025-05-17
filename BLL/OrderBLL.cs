using System;
using System.Collections.Generic;
using System.Linq;
using QLBS.DTOs.Order;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBS.DAL;
using System.Data.Entity;
using QLBS.Views.Customer.Buy;


namespace QLBS.BLL
{
    public class OrderBLL
    {
        private QLBSDbContext _context;
        public OrderBLL() { 
            _context = new QLBSDbContext();
        }
        private static OrderBLL _instance;
        public static OrderBLL getInstance()
        {
            if (_instance == null)
            {
                _instance = new OrderBLL();
            }
            return _instance;
        }
        public bool AddOrderWithDetails(OrderCreateDTO orderDTO, List<CartItem> Cart)
        {
            if (!_context.Users.Any(u => u.ID == orderDTO.UserId))
            {
                return false;
            }

            // Tạo đơn hàng mới
            var order = new Order
            {
                UserId = orderDTO.UserId,
                OrderDate = orderDTO.OrderDate,
                TotalPrice = orderDTO.TotalAmount
            };

            _context.Orders.Add(order);
            _context.SaveChanges(); // Lưu để lấy OrderId

            // Thêm chi tiết đơn hàng
            foreach (var item in Cart)
            {
                // Kiểm tra tồn kho
                var book = _context.Books.Find(item.Book.Id);
                if (book == null || item.Quantity > book.Stock)
                {
                    return false;
                }

                // Tạo chi tiết đơn hàng
                var orderDetail = new OrderDetail
                {
                    OrderId = order.ID,
                    BookId = item.Book.Id,
                    quantity = item.Quantity,
                    price = book.Price
                };

                // Cập nhật số lượng tồn kho
                book.Stock -= item.Quantity;

                _context.OrderDetails.Add(orderDetail);
            }

            _context.SaveChanges();
            return true;
        }
        public List<OrderDTO> GetOrdersByUserId(int userId) 
        {
            return _context.Orders
                        .Where(o => o.UserId == userId)
                        .Include(o => o.User)
                        .Select(o => new OrderDTO
                        {
                            Id = o.ID,
                            UserId = o.UserId,
                            UserName = o.User.Name,
                            OrderDate = o.OrderDate,
                            TotalAmount = o.TotalPrice
                        })
                            .ToList();
        }
        public List<OrderDTO> GetAllOrders()
        {
            return _context.Orders
                            .Include(o => o.User)
                            .Select(o => new OrderDTO
                            {
                                Id = o.ID,
                                UserId = o.UserId,
                                UserName = o.User.Name,
                                OrderDate = o.OrderDate,
                                TotalAmount = o.TotalPrice
                            })
                            .ToList();
        }
        public List<OrderDTO> GetOrderByUserName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return GetAllOrders();
            }

            return _context.Orders
                .Include(o => o.User)
                .Where(o => o.User.Name.ToLower().Contains(name.ToLower()))
                .Select(o => new OrderDTO
                {
                    Id = o.ID,
                    UserId = o.UserId,
                    UserName = o.User.Name,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalPrice
                })
                .ToList();
        }
    }
}
