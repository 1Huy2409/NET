using QLBS.BLL;
using QLBS.DAL;
using QLBS.DTOs.User;
using QLBS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBS.Views.Admin.CRUDCustomer
{
    public partial class EditCustomer : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        private int id;
        private CustomerDTO editCustomer;
        public EditCustomer(int Id, MyDel d)
        {
            this.id = Id;
            this.d = d;
            this.editCustomer = CustomerBLL.getInstance().GetCustomerById(this.id);
            InitializeComponent();
            LoadEdit();
        }
        private void LoadEdit()
        {
            txtName.Text = this.editCustomer.Name;
            txtEmail.Text = this.editCustomer.Email;
            txtUserName.Text = this.editCustomer.UserName;
            txtPhone.Text = this.editCustomer.Phone;
            txtAddress.Text = this.editCustomer.Address;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            // thực hiện logic
            var newCustomer = new CustomerUpdateDTO
            {
                Id = this.id,
                UserName = txtUserName.Text.Trim(),
                Name = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Address = txtAddress.Text.Trim()
            };
            if (!ValidationHelper.Validate(newCustomer, this, errorProvider1))
            {
                return;
            }
            bool checkEdit = CustomerBLL.getInstance().UpdateCustomer(newCustomer);
            if (checkEdit)
            {
                MessageBox.Show("Edit thành công!");
                d();
                this.Close();
            }
            else
            {
                MessageBox.Show("Edit thất bại!");
            }    
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
