using QLBS.BLL;
using QLBS.DAL;
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

namespace QLBS.Views
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // validate dữ liệu
            var newUser = new User
            {
                Name = txtFName.Text,
                Email = txtEmail.Text,
                UserName = txtUserName.Text,
                Password = txtPassword.Text,
                Address = txtAddress.Text,
                Phone = txtPhone.Text,
                Role = "Customer"
            };
            // validate dữ liệu dựa vào data annotation trong domain class
            if (!ValidationHelper.Validate(newUser, this, errorProvider1))
            {
                return;
            }
            // check username trùng và email trùng
            if (!AuthBLL.getInstance().IsEmailExist(newUser.Email))
            {
                errorProvider1.SetError(txtEmail, "Email đã được đăng ký");
                MessageBox.Show("Email đã được đăng ký", "Lỗi đăng ký",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!AuthBLL.getInstance().IsUserNameExist(newUser.UserName))
            {
                errorProvider1.SetError(txtUserName, "Tên đăng nhập đã tồn tại");
                MessageBox.Show("Tên đăng nhập đã tồn tại", "Lỗi đăng ký",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool checkRegister = AuthBLL.getInstance().Register(newUser);
            if (checkRegister)
            {
                MessageBox.Show("Đăng ký tài khoản thành công! Vui lòng đăng nhập");
                this.Close();
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại! Vui lòng thử lại!");
                this.Close();
            }
        }
    }
}
