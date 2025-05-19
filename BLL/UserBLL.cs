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
        private readonly UserDAL _userDAL;
        private static UserBLL _instance;

        public UserBLL() 
        {
            _userDAL = new UserDAL();
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
            var user = _userDAL.GetUserByUsername(loginDTO.UserName);
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
                Role = user.Role
            };
        }
        // service cho register
        public bool Register(UserRegisterDTO registerDTO)
        {
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

            if (!_userDAL.AddUser(user))
            {
                return false;
            }
            return true;
        }
        public bool updateUser(UserEditDTO userDTO)
        {
            var user = _userDAL.GetUserById(userDTO.Id);
            if (user == null) return false;

            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.Phone = userDTO.Phone;
            user.Address = userDTO.Address;
            user.UserName = userDTO.UserName;

            if (!_userDAL.UpdateUser(user))
            {
                return false;
            }

            SessionManager.CurrentUser = user;
            return true;
        }
        public bool UpdatePassword(string username, string newPassword)
        {
            var user = _userDAL.GetUserByUsername(username);
            if (user == null) return false;

            user.Password = PasswordUtils.getInstance().HashPassword(newPassword);
            SessionManager.CurrentUser = user;
            return _userDAL.UpdateUser(user);
        }
    }
}
