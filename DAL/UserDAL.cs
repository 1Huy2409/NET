using QLBS.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace QLBS.DAL
{
    public class UserDAL
    {
        private readonly QLBSDbContext _context;

        public UserDAL()
        {
            _context = new QLBSDbContext();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }

        public bool AddUser(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                MessageBox.Show("Email này đã tồn tại");
                return false;
            }
            if (_context.Users.Any(u => u.UserName == user.UserName))
            {
                MessageBox.Show("Tên người dùng này đã tồn tại");
                return false;
            }
            if (_context.Users.Any(u => u.Phone == user.Phone))
            {
                MessageBox.Show("Số điện thoại này đã tồn tại");
                return false;
            }
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateUser(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email && u.ID != user.ID))
            {
                MessageBox.Show("Email này đã tồn tại");
                return false;
            }
            if (_context.Users.Any(u => u.Phone == user.Phone && u.ID != user.ID))
            {
                MessageBox.Show("Số điện thoại này đã tồn tại");
                return false;
            }
            if (_context.Users.Any(u => u.UserName == user.UserName && u.ID != user.ID))
            {
                MessageBox.Show("Tên người dùng này đã tồn tại");
                return false;
            }
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUsers(List<int> userIds)
        {
            foreach (var id in userIds)
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                }
            }
            _context.SaveChanges();
            return true;
        }

        public List<User> SearchUsers(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return _context.Users.Where(u => u.Role == "Customer").ToList();
            }

            keyword = keyword.ToLower();
            return _context.Users
                .Where(u => u.Role == "Customer" && (
                    u.Name.ToLower().Contains(keyword) ||
                    u.Email.ToLower().Contains(keyword) ||
                    u.Phone.Contains(keyword) ||
                    u.Address.ToLower().Contains(keyword)
                ))
                .ToList();
        }
        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }
    }
}