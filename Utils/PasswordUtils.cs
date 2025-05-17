using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace QLBS.Utils
{
    public class PasswordUtils
    {
        private static PasswordUtils _instance;
        public static PasswordUtils getInstance()
        {
            if (_instance == null)
            {
                _instance = new PasswordUtils();
            }    
            return _instance;
        }
        public string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }
        public bool VerifyPassword(string password, string hashedPassword) 
        {
            bool check = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return check;
        }
    }
}
