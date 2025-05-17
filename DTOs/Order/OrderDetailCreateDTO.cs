using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.DTOs.Order
{
    public class OrderDetailCreateDTO
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
