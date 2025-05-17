using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBS.DAL;
using QLBS.Utils;

namespace QLBS.BLL
{
    
    public class AuthBLL
    {
        private QLBSDbContext _context;
        private static AuthBLL _instance;
        public static AuthBLL getInstance()
        {
            if ( _instance == null )
            {
                _instance = new AuthBLL();
            }
            return _instance;
        }
        public AuthBLL()
        {
            _context = new QLBSDbContext();
        }
        public bool UpdatePassword(string username, string newPassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                // ko tìm thấy user
                return false;
            }
            string newHashedPassword = PasswordUtils.getInstance().HashPassword(newPassword);
            user.Password = newHashedPassword;
            SessionManager.CurrentUser = user;
            _context.SaveChanges();
            return true;
        }
        public bool Login(string username, string password)
        {
            var user = _context.Users
                            .FirstOrDefault(u => u.UserName == username);
            if ( user == null ) {
                // ko tìm thấy user
                return false;
            }
            bool isValidPassword = PasswordUtils.getInstance().VerifyPassword(password, user.Password);
            if ( !isValidPassword ) {
                // mat khau sai 
                return false;
            }
            SessionManager.CurrentUser = user;
            return true;
        }
        public bool IsEmailExist(string email, int? id = null)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.ID != (id ?? -1));
            if (user != null)
            {
                return false;
            }
            return true;

        }
        public bool IsUserNameExist(string username, int? id = null)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username && u.ID != (id ?? -1));
            if (user != null)
            {
                return false;
            }
            return true;
        }
        public bool Register(User user)
        {
            try
            {
                // Validate dữ liệu đầu vào
                if (user == null)
                {
                    Console.WriteLine("Thông tin người dùng không hợp lệ");
                    return false;
                }

                // Kiểm tra trùng username
                if (_context.Users.Any(u => u.UserName == user.UserName))
                {
                    Console.WriteLine("Tên người dùng đã tồn tại!");
                    return false;
                }

                // Kiểm tra trùng email
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    Console.WriteLine("Email này đã tồn tại");
                    return false;
                }

                // Kiểm tra trùng số điện thoại (sửa lỗi logic)
                if (_context.Users.Any(u => u.Phone == user.Phone))
                {
                    Console.WriteLine("Số điện thoại này đã tồn tại");
                    return false;
                }

                // Mã hóa password
                user.Password = PasswordUtils.getInstance().HashPassword(user.Password);

                // Thêm user và lưu vào database
                _context.Users.Add(user);
                _context.SaveChanges();

                Console.WriteLine("Đăng ký thành công!");
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                // Xử lý lỗi validation
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi khi dang ky: {ex.Message}");
                return false;
            }
        }

    }
}
