using QLBS.Utils;
using QLBS.Views.Admin.CRUDCategory;
using QLBS.Views.Admin.CRUDCustomer;
using QLBS.Views.Admin.CRUDOrder;
using QLBS.Views.Customer.Info;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBS.Views.Admin
{
    public partial class Dashboard : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        public Dashboard(MyDel d)
        {
            this.d = d;
            InitializeComponent();
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            d();
            this.Close();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            ListCustomer listCustomerForm = new ListCustomer();
            listCustomerForm.Show();
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            ListBooks listBooksForm = new ListBooks();
            listBooksForm.Show();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            ListOrder listOrder = new ListOrder();
            listOrder.Show();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            ListCategory listCategory = new ListCategory(); 
            listCategory.Show();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            CustomerInfo customerInfo = new CustomerInfo();
            customerInfo.Show();
        }
    }
}
