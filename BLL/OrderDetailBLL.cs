using QLBS.DAL;
using QLBS.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace QLBS.BLL
{
    public class OrderDetailBLL
    {
        private QLBSDbContext _context;
        public OrderDetailBLL()
        {
            _context = new QLBSDbContext();
        }
        private static OrderDetailBLL _instance;
        public static OrderDetailBLL getInstance()
        {
            if (_instance == null)
            {
                _instance = new OrderDetailBLL();
            }
            return _instance;
        }
        public List<OrderDetailDTO> GetOrderDetails(int orderId) 
        {
            return _context.OrderDetails
            .Include(od => od.Book)
            .Where(od => od.OrderId == orderId)
            .Select(od => new OrderDetailDTO
            {
                Id = od.ID,
                OrderId = od.OrderId,
                BookId = od.BookId,
                BookTitle = od.Book.Title,
                Quantity = od.quantity,
                Price = od.price,
                Subtotal = od.quantity * od.price
            })
            .ToList();
        }
    }
}
