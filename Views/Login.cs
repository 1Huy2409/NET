using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBS.BLL;
using QLBS.DTOs.User;
using QLBS.Utils;
using QLBS.Views.Admin;
using QLBS.Views.Customer;

namespace QLBS.Views
{
    public partial class Login : Form
    {
        public Login()
        {
            SessionManager.CurrentUser = null;
            InitializeComponent();
        }
        public void ResetField()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register registerForm = new Register();
            registerForm.ShowDialog();
            this.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // validate dữ liệu
            if (!Validation.ValidateLogin(txtUserName.Text, txtPassword.Text))
            {
                return;
            }
            // lấy value từ user name và password đưa vào bll authcontroller để kiểm tra
            var userLogin = new UserLoginDTO
            {
                UserName = txtUserName.Text,
                Password = txtPassword.Text,
            };
            UserDTO user = UserBLL.getInstance().Login(userLogin);
            if (user!=null)
            {
                // chuyen sang form khac
                if (SessionManager.CurrentUser.Role == "Admin")
                {
                    this.Hide();
                    Admin.Dashboard dashboard = new Admin.Dashboard(ResetField);
                    dashboard.ShowDialog();
                    this.Show();
                }
                if (SessionManager.CurrentUser.Role == "Customer")
                {
                    this.Hide();
                    Customer.Dashboard dashboard = new Customer.Dashboard(ResetField);
                    dashboard.ShowDialog();
                    this.Show();
                }
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại! Vui lòng thử lại!");
            }
        }
    }
}
