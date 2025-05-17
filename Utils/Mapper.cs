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
    }
}
