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
        // after DTOs
        public List<CustomerDTO> getAllCustomers()
        {
            var customers = _context.Users.Where(u => u.Role == "Customer").ToList();
            return customers.Select(c => new CustomerDTO
            {
                Id = c.ID,
                UserName = c.UserName,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address
            }).ToList();
        }
        public CustomerDTO GetCustomerById(int id)
        {
            var customer = _context.Users
                .FirstOrDefault(u => u.ID == id && u.Role == "Customer");

            if (customer == null) return null;

            return new CustomerDTO
            {
                Id = customer.ID,
                UserName = customer.UserName,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            };
        }
        public bool CreateCustomer(CustomerCreateDTO customer)
        {
            // kiểm tra username và email đã tồn tại hay chưa
            if (_context.Users.Any(u => u.Email == customer.Email))
            {
                MessageBox.Show("Email này đã tồn tại!");
                return false;   
            }
            if (_context.Users.Any(u => u.UserName == customer.UserName))
            {
                MessageBox.Show("Tên người dùng này đã tồn tại!");
                return false;
            }
            User user = new User
            {
                UserName = customer.UserName,
                Password = PasswordUtils.getInstance().HashPassword(customer.Password),
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                Name = customer.Name,
                Role = "Customer"
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateCustomer(CustomerUpdateDTO customer)
        {
            var findCustomer = _context.Users.Find(customer.Id); // tìm customer cần update
            if (findCustomer == null || findCustomer.Role != "Customer")
            {
                MessageBox.Show("Không tìm thấy người dùng!");
                return false;
            }
            if (_context.Users.Any(u => u.Email == customer.Email && u.ID != customer.Id))
            {
                MessageBox.Show("Email này đã tồn tại!");
                return false;
            }
            if (_context.Users.Any(u => u.UserName == customer.UserName && u.ID != customer.Id))
            {
                MessageBox.Show("Tên người dùng này đã tồn tại!");
                return false;
            }
            findCustomer.Name = customer.Name;
            findCustomer.Email = customer.Email;
            findCustomer.Phone = customer.Phone;
            findCustomer.Address = customer.Address;
            findCustomer.UserName = customer.UserName;
            _context.SaveChanges();
            return true;
        }
        public bool DeleteCustomers(List<int> delCustomerIds)
        {
            for (int i = 0; i < delCustomerIds.Count; i++)
            { 
                int id = delCustomerIds[i];
                User delCustomer = _context.Users.Find(id);
                if (delCustomer != null)
                {
                    _context.Users.Remove(delCustomer);
                    _context.SaveChanges();
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public List<CustomerDTO> SearchCustomers(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return getAllCustomers();
            }

            keyword = keyword.ToLower();
            return _context.Users
                .Where(u => u.Role == "Customer" && (
                    u.Name.ToLower().Contains(keyword) ||
                    u.Email.ToLower().Contains(keyword) ||
                    u.Phone.Contains(keyword) ||
                    u.Address.ToLower().Contains(keyword)
                ))
                .Select(u => new CustomerDTO
                {
                    Id = u.ID,
                    UserName = u.UserName,
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    Address = u.Address
                })
                .ToList();
        }
        // end after DTOs
    }
}
