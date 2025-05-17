using QLBS.DAL;
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
        public void AddOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }
        public void RemoveOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Remove(orderDetail);
            _context.SaveChanges();
        }
        public List<OrderDetail> GetOrderDetails(int orderId) 
        {
            return _context.OrderDetails
                        .Where(od => od.OrderId == orderId)
                        .Include(od => od.Book)
                        .ToList();
        }
    }
}
