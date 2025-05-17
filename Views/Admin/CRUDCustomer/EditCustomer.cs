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

namespace QLBS.Views.Admin.CRUDCustomer
{
    public partial class EditCustomer : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        private int id;
        private User editCustomer;
        public EditCustomer(int Id, MyDel d)
        {
            this.id = Id;
            this.d = d;
            this.editCustomer = CustomerBLL.getInstance().getUserById(Id);
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
            User newCustomer = new User
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                UserName = txtUserName.Text,
                Phone = txtPhone.Text,
                Address = txtAddress.Text,
                Password = this.editCustomer.Password,
                Role = this.editCustomer.Role
            };
            if (!ValidationHelper.Validate(newCustomer, this, errorProvider1))
            {
                return;
            }
            // check username trùng và email trùng
            if (!AuthBLL.getInstance().IsEmailExist(newCustomer.Email, id))
            {
                errorProvider1.SetError(txtEmail, "Email đã được đăng ký");
                MessageBox.Show("Email đã được đăng ký", "Lỗi đăng ký",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!AuthBLL.getInstance().IsUserNameExist(newCustomer.UserName, id))
            {
                errorProvider1.SetError(txtUserName, "Tên đăng nhập đã tồn tại");
                MessageBox.Show("Tên đăng nhập đã tồn tại", "Lỗi đăng ký",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool checkEdit = CustomerBLL.getInstance().editUser(id, newCustomer);
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
