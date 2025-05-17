using QLBS.DAL;
using QLBS.DAL.Entities;
using QLBS.DTOs.Book;
using QLBS.DTOs.Category;
using QLBS.DTOs.Order;
using QLBS.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.Utils
{
    public class Mapper
    {
        public static UserDTO ToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.ID,
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                Role = user.Role
            };
        }
        public static BookDTO ToDTO(Book book)
        {
            return new BookDTO
            {
                Id = book.ID,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                Stock = book.Stock,
                CategoryName = book.Category?.Name,
                ImageUrl = book.ImageUrl
            };
        }
        public static CategoryDTO ToDTO(Category category)
        {
            return new CategoryDTO
            {
                Id = category.CategoryId,
                Name = category.Name,
            };
        }
        public static OrderDTO ToDTO(Order order)
        {
            return new OrderDTO
            {
                Id = order.ID,
                UserName = order.User.UserName,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalPrice,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDTO
                {
                    Id = od.ID,
                    BookId = od.BookId,
                    BookTitle = od.Book.Title,
                    Quantity = od.quantity,
                    Price = od.price,
                    Subtotal = od.price * od.quantity
                }).ToList(),
            };
        }
    }
}
