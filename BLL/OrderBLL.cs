using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBS.DAL;
using System.Data.Entity;


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
        public void AddOrderWithDetails(Order order, List<CartItem> Cart)
        {
            order.OrderDetails = new List<OrderDetail>();   

            foreach (CartItem item in Cart)
            {
                Console.WriteLine(item.Book.Title.ToString());
                if (item.Quantity > item.Book.Stock)
                {
                    MessageBox.Show("Không đủ hàng tồn kho!");
                    return;
                }
                OrderDetail detail = new OrderDetail
                {
                    BookId = item.Book.ID,
                    quantity = item.Quantity,
                    price = item.Book.Price,
                    OrderId = order.ID
                };
                // tru ton kho
                Book bookInDB = _context.Books.Find(item.Book.ID);
                bookInDB.Stock -= item.Quantity;
                order.OrderDetails.Add(detail);
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public List<Order> GetOrdersByUserId(int userId) 
        {
            return _context.Orders
                        .Where(o => o.UserId == userId)
                        .Include(o => o.User)
                        .ToList();
        }
        public List<Order> GetAllOrders()
        {
                return _context.Orders
                              .Include(o => o.User)
                              .Include(o => o.OrderDetails.Select(od => od.Book))
                              .ToList();
        }
        public Order GetOrderById(int id)
        {
            return _context.Orders
                           .Where(o => o.ID == id)
                           .Include(o => o.User)
                           .Include(o => o.OrderDetails.Select(od => od.Book)).FirstOrDefault();
        }
        public List<Order> GetOrderByUserName(string name)
        {
            return _context.Orders
                           .Where(o => o.User.Name.ToLower().Contains(name))
                           .Include(o => o.User)
                           .Include(o => o.OrderDetails.Select(od => od.Book)).ToList();
        }
    }
}
