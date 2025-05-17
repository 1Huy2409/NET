using QLBS.DAL;
using QLBS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.BLL
{
    public class CustomerBLL
    {
        private QLBSDbContext _context;
        private static CustomerBLL _instance;
        public static CustomerBLL getInstance()
        {
            if (_instance == null)
            {
                _instance = new CustomerBLL();
            }
            return _instance;
        }
        public CustomerBLL()
        {
            _context = new QLBSDbContext();
        }
        public List<User> getAllCustomers()
        {
            return _context.Users
                .Where(u => u.Role == "Customer")
                .ToList();
        }
        public void addCustomer(User user)
        {
            user.Password = PasswordUtils.getInstance().HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void removeCustomer(List<int> delUserIds) 
        { 
            for (int i = 0; i <  delUserIds.Count; i++)
            {
                int id = delUserIds[i];
                User delUser = _context.Users.Find(id);
                if (delUser != null)
                {
                    _context.Users.Remove(delUser);
                    _context.SaveChanges();
                }
            }
        }
        public User getUserById (int id)
        {
            return _context.Users.Find(id);
        }
        public bool editUser (int id, User newCustomer)
        {
            User editCustomer = _context.Users.Find(id);
            if (editCustomer != null)
            {
                editCustomer.Address = newCustomer.Address;
                editCustomer.Name = newCustomer.Name;
                editCustomer.Email = newCustomer.Email;
                editCustomer.Phone = newCustomer.Phone;
                editCustomer.Password = newCustomer.Password;
                editCustomer.Role = newCustomer.Role;
                editCustomer.UserName = newCustomer.UserName;
                _context.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }
        public List<User> getCustomerByName(string name)
        {
            return _context.Users
                    .Where(u => u.Name.ToLower().Contains(name) && u.Role == "Customer").ToList();
        }
    }
}
