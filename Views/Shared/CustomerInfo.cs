using QLBS.BLL;
using QLBS.DAL;
using QLBS.Utils;
using QLBS.DTOs.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBS.Views.Customer.Info
{
    public partial class CustomerInfo : Form
    {
        private bool isEdited = false;
        private bool isChangPw = false;
        private bool isConfirm = false;
        public CustomerInfo()
        {
            InitializeComponent();
            LoadInfo();
        }
        public void LoadInfo()
        {
            txtFullName.Text = SessionManager.CurrentUser.Name;
            txtEmail.Text = SessionManager.CurrentUser.Email;
            txtUserName.Text = SessionManager.CurrentUser.UserName;
            txtPhone.Text = SessionManager.CurrentUser.Phone;
            txtAddress.Text = SessionManager.CurrentUser.Address;

            txtFullName.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtUserName.ReadOnly = true;
            txtPhone.ReadOnly = true;
            txtAddress.ReadOnly = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            isEdited = true;
            txtFullName.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtUserName.ReadOnly = false;
            txtPhone.ReadOnly = false;
            txtAddress.ReadOnly = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (isChangPw && !isConfirm)
            {
                MessageBox.Show("Vui lòng xác nhận ở phần đổi mật khẩu!");
                return;
            }
            if (isEdited)
            {
                // thực hiện cập nhật cho customer này  
                UserEditDTO user = new UserEditDTO
                {
                    Id = SessionManager.CurrentUser.ID,
                    Name = txtFullName.Text,
                    Email = txtEmail.Text,
                    UserName = txtUserName.Text,
                    Phone = txtPhone.Text,
                    Address = txtAddress.Text,
                };
                if (!ValidationHelper.Validate(user, this, errorProvider1))
                {
                    return;
                }    
                bool checkEdit = UserBLL.getInstance().updateUser(user);
                if (checkEdit)
                {
                    // update thanh cong
                    MessageBox.Show("Cập nhật thông tin thành công!");
                }
                else
                {
                    // update thất bại
                    MessageBox.Show("Cập nhật thông tin thất bại!");
                }
            }
            this.Close();
        }

        private void btnChangePw_Click(object sender, EventArgs e)
        {
            this.isChangPw = true;
            lbCurrentPw.Visible = true;
            lbNewPw.Visible = true;
            lbConfirmPw.Visible = true;
            txtCurrentPw.Visible=true;
            txtNewPw.Visible=true;
            txtConfirmPw.Visible=true;
            btnConfirm.Visible=true;
        }
        private bool checkPassword()
        {
            if (txtCurrentPw.Text.Trim() == "" || txtNewPw.Text.Trim() == "" || txtConfirmPw.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin để đổi mật khẩu!");
                return false;
            }    
            // check valid old pw
            if (!PasswordUtils.getInstance().VerifyPassword(txtCurrentPw.Text, SessionManager.CurrentUser.Password))
            {
                MessageBox.Show("Mật khẩu cũ không đúng!");
                return false;
            }
            if (txtNewPw.Text.Trim().Length < 6)
            {
                MessageBox.Show("Mật khẩu yêu cầu tối thiểu 6 ký tự!");
                return false;
            }    
            if (txtNewPw.Text != txtConfirmPw.Text)
            {
                MessageBox.Show("Xác nhận lại mật khẩu mới không đúng!");
                return false;
            }
            return true;
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // true => thực hiện đổi mật khẩu dựa vào text
            if (!checkPassword())
            {
                return;
            }
            string newPassword = txtNewPw.Text.Trim();
            bool checkUpdate = UserBLL.getInstance().UpdatePassword(SessionManager.CurrentUser.UserName, newPassword);
            if (checkUpdate)
            {
                MessageBox.Show("Đổi mật khẩu thành công!");
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại!");
            }
            this.isConfirm = true;
        }
    }
}
