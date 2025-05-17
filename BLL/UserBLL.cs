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
            var user = _context.Users.FirstOrDefault(u => u.UserName == loginDTO.UserName && u.Password == loginDTO.Password);
            return user != null ? Mapper.ToDTO(user) : null;
        }
        // service cho register
        public bool Register(UserRegisterDTO registerDTO)
        {
            // kiểm tra xem email đã tồn tại hay chưa
            if (_context.Users.Any(u => u.Email == registerDTO.Email))
            {
                return false;
            }
            // kiểm tra xem username đã tồn tại hay chưa
            if (_context.Users.Any(u => u.UserName == registerDTO.UserName))
            {
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
        public bool updateUser(int id, User user)
        {
            var findUser = _context.Users.Find(id);
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
    }
}
