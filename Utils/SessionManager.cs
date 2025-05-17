using QLBS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.Utils
{
    public class SessionManager
    {
        public static User CurrentUser { get; set; }
        public static List<CartItem> Cart { get; set; } = new List<CartItem>();
    }
}
