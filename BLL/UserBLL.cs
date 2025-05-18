using QLBS.DAL;
using QLBS.DTOs.User;
using QLBS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBS.BLL
{
    public class UserBLL
    {
        // singleton pattern
        private QLBSDbContext _context;
        private static UserBLL _instance;

        public UserBLL() 
        {
            _context = new QLBSDbContext(); 
        }
        public static UserBLL getInstance()
        {
            if (_instance == null)
            {
                _instance = new UserBLL();
            }
            return _instance;
        }
        // service cho login
        public UserDTO Login(UserLoginDTO loginDTO)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == loginDTO.UserName); // lấy phần tử đầu tiên
            if (user == null)
            {
                MessageBox.Show("Không tìm thấy username này!");
                return null;
            }
            bool isValidPassword = PasswordUtils.getInstance().VerifyPassword(loginDTO.Password, user.Password);
            if (!isValidPassword)
            {
                MessageBox.Show("Mật khẩu sai!");
                return null;
            }
            SessionManager.CurrentUser = user;
            return new UserDTO
            {
                Id = user.ID,
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                Role = user.Role,
            };
        }
        // service cho register
        public bool Register(UserRegisterDTO registerDTO)
        {
            // kiểm tra xem email đã tồn tại hay chưa
            if (_context.Users.Any(u => u.Email == registerDTO.Email))
            {
                MessageBox.Show("Email này đã tồn tại!");
                return false;
            }
            // kiểm tra xem username đã tồn tại hay chưa
            if (_context.Users.Any(u => u.UserName == registerDTO.UserName))
            {
                MessageBox.Show("Tên người dùng này đã tồn tại!");
                return false;
            }
            var user = new User
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                Password = PasswordUtils.getInstance().HashPassword(registerDTO.Password),
                Name = registerDTO.Name,
                Phone = registerDTO.Phone,
                Address = registerDTO.Address,
                Role = "Customer"
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }
        public bool updateUser(UserEditDTO user)
        {
            if (_context.Users.Any(u => u.Email == user.Email && u.ID != user.Id))
            {
                MessageBox.Show("Email này đã tồn tại!");
                return false;
            }
            if (_context.Users.Any(u => u.UserName == user.UserName && u.ID != user.Id))
            {
                MessageBox.Show("Username này đã tồn tại!");
                return false;
            }
            if (_context.Users.Any(u => u.Phone == user.Phone && u.ID != user.Id))
            {
                MessageBox.Show("Số điện thoại này đã tồn tại!");
                return false;
            }
            var findUser = _context.Users.Find(user.Id);
            if (findUser != null) {
                findUser.Address = user.Address;
                findUser.Name = user.Name;
                findUser.Email = user.Email;
                findUser.Phone = user.Phone;
                findUser.UserName = user.UserName;
                _context.SaveChanges();
            }
            else
            {
                return false;
            }
            SessionManager.CurrentUser = findUser;
            return true;
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
    }
}
