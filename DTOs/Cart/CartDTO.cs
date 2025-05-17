using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.DTOs.Cart
{
    public class CartDTO
    {
        public List<CartItemDTO> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
