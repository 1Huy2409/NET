using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBS.DAL;
using System.Windows.Forms;
using QLBS.BLL;
using QLBS.Utils;

namespace QLBS.Views.Admin.CRUDCustomer
{
    public partial class AddCustomer : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        public AddCustomer(MyDel d)
        {
            this.d = d;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            User newCustomer = new User
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                UserName = txtUserName.Text,
                Password = txtPassword.Text,
                Address = txtAddress.Text,
                Role = "Customer",
                Phone = txtPhone.Text,
            };
            // validate new user
            if (!ValidationHelper.Validate(newCustomer, this, errorProvider1))
            {
                return;
            }
            // check username trùng và email trùng
            if (!AuthBLL.getInstance().IsEmailExist(newCustomer.Email))
            {
                errorProvider1.SetError(txtEmail, "Email đã được đăng ký");
                MessageBox.Show("Email đã được đăng ký", "Lỗi đăng ký",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!AuthBLL.getInstance().IsUserNameExist(newCustomer.UserName))
            {
                errorProvider1.SetError(txtUserName, "Tên đăng nhập đã tồn tại");
                MessageBox.Show("Tên đăng nhập đã tồn tại", "Lỗi đăng ký",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CustomerBLL.getInstance().addCustomer(newCustomer);
            d();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
