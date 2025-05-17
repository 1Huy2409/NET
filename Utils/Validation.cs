using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace QLBS.Utils
{
    public class Validation
    {
        // validate login form
        public static bool ValidateLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập UserName!");
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập Password!");
                return false;
            }
            return true;
        }
        // validate register form
        public static bool ValidateRegister(string fullname, string email, string username, string password, string address, string phone) 
        {
            if (string.IsNullOrEmpty (fullname)) 
            {
                MessageBox.Show("Vui lòng nhập tên tối thiểu 10 ký tự!");
                return false;
            }
            if (string.IsNullOrEmpty (email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ! Vui lòng nhập lại!");
                return false;
            }
            if (string.IsNullOrEmpty(username) || username.Length < 4)
            {
                MessageBox.Show("Tên người dùng không được dưới 4 ký tự!");
                return false;
            }
            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                MessageBox.Show("Mật khẩu không được dưới 6 ký tự!");
                return false;
            }
            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Địa chỉ không được bỏ trống!");
                return false;
            }
            phone = phone.Trim();
            if (!Regex.IsMatch(phone, @"^(0|\+84)[3|5|7|8|9]\d{8}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!");
                return false;
            }
            return true;
        }
    }
}
