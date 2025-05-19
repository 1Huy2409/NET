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
        private readonly OrderDAL _orderDAL;
        public OrderBLL() {
            _orderDAL = new OrderDAL();
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
            var order = new Order
            {
                UserId = orderDTO.UserId,
                OrderDate = orderDTO.OrderDate,
                TotalPrice = orderDTO.TotalAmount
            };

            var orderDetails = Cart.Select(item => new OrderDetail
            {
                BookId = item.Book.Id,
                quantity = item.Quantity,
                price = item.Book.Price
            }).ToList();

            return _orderDAL.AddOrderWithDetails(order, orderDetails);
        }
        public List<OrderDTO> GetOrdersByUserId(int userId) 
        {
            var orders = _orderDAL.GetOrdersByUserId(userId);
            return orders.Select(o => new OrderDTO
            {
                Id = o.ID,
                UserId = o.UserId,
                UserName = o.User.Name,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalPrice
            }).ToList();
        }
        public List<OrderDTO> GetAllOrders()
        {
            var orders = _orderDAL.GetAllOrders();
            return orders.Select(o => new OrderDTO
            {
                Id = o.ID,
                UserId = o.UserId,
                UserName = o.User.Name,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalPrice
            }).ToList();
        }
        public List<OrderDTO> GetOrderByUserName(string name)
        {
            var orders = _orderDAL.GetOrdersByUserName(name);
            return orders.Select(o => new OrderDTO
            {
                Id = o.ID,
                UserId = o.UserId,
                UserName = o.User.Name,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalPrice
            }).ToList();
        }
    }
}
