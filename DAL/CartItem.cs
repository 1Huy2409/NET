using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBS.DTOs.Book;

namespace QLBS.DAL
{
    public class CartItem
    {
        public BookDTO Book { get; set; }
        public int Quantity { get; set; }
    }
}
