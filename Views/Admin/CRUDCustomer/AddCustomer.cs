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
using QLBS.DTOs.User;

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
            var newCustomer = new CustomerCreateDTO
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                UserName = txtUserName.Text,
                Password = txtPassword.Text,
                Address = txtAddress.Text,
                Phone = txtPhone.Text,
            };
            // validate new user
            if (!ValidationHelper.Validate(newCustomer, this, errorProvider1))
            {
                return;
            }
            if (CustomerBLL.getInstance().CreateCustomer(newCustomer))
            {
                MessageBox.Show("Thêm khách hàng thành công!");
                d();
                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm khách hàng thất bại!");
                return;
            }    
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
