using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.DTOs.Order
{
    public class OrderCreateDTO
    {
        public int UserId { get; set; }
        public List<OrderDetailCreateDTO> OrderDetails { get; set; }
    }
}
