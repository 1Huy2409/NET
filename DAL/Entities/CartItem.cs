using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.DAL
{
    public class CartItem
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
