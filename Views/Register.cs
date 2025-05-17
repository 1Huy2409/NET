using QLBS.BLL;
using QLBS.DTOs.User;
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
            var newUser = new UserRegisterDTO
            {
                UserName = txtUserName.Text,
                Password = txtPassword.Text,
                Name = txtFName.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text,
                Address = txtAddress.Text,  
            };
            // validate dữ liệu dựa vào data annotation trong domain class
            if (!ValidationHelper.Validate(newUser, this, errorProvider1))
            {
                return;
            }
            bool checkRegister = UserBLL.getInstance().Register(newUser);
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
