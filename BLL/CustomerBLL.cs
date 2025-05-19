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
        private readonly UserDAL _userDAL;
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
            _userDAL = new UserDAL();
        }
        // after DTOs
        public List<CustomerDTO> getAllCustomers()
        {
            var customers = _userDAL.SearchUsers("");
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
            var customer = _userDAL.GetUserById(id);
            if (customer == null || customer.Role != "Customer") return null;

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
        public bool CreateCustomer(CustomerCreateDTO customerDTO)
        {
            var user = new User
            {
                UserName = customerDTO.UserName,
                Password = PasswordUtils.getInstance().HashPassword(customerDTO.Password),
                Email = customerDTO.Email,
                Phone = customerDTO.Phone,
                Address = customerDTO.Address,
                Name = customerDTO.Name,
                Role = "Customer"
            };

            if (!_userDAL.AddUser(user))
            {
                return false;
            }
            return true;
        }
        public bool UpdateCustomer(CustomerUpdateDTO customerDTO)
        {
            var customer = _userDAL.GetUserById(customerDTO.Id);
            if (customer == null || customer.Role != "Customer")
            {
                MessageBox.Show("Không tìm thấy khách hàng!");
                return false;
            }

            customer.Name = customerDTO.Name;
            customer.Email = customerDTO.Email;
            customer.Phone = customerDTO.Phone;
            customer.Address = customerDTO.Address;
            customer.UserName = customerDTO.UserName;

            if (!_userDAL.UpdateUser(customer))
            {
                return false;
            }
            return true;
        }
        public bool DeleteCustomers(List<int> delCustomerIds)
        {
            return _userDAL.DeleteUsers(delCustomerIds);
        }
        public List<CustomerDTO> SearchCustomers(string keyword)
        {
            var customers = _userDAL.SearchUsers(keyword);
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
        // end after DTOs
    }
}
