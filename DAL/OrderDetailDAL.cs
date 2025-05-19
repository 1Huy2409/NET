using QLBS.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace QLBS.DAL
{
    public class OrderDetailDAL
    {
        private readonly QLBSDbContext _context;

        public OrderDetailDAL()
        {
            _context = new QLBSDbContext();
        }

        public List<OrderDetail> GetOrderDetails(int orderId)
        {
            return _context.OrderDetails
                .Include(od => od.Book)
                .Where(od => od.OrderId == orderId)
                .ToList();
        }
    }
}