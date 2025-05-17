using QLBS.BLL;
using QLBS.DAL;
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
    public partial class ListCustomer : Form
    {
        private List<User> defaultCustomer = new List<User> ();
        private List<User> currentCustomer = new List<User> ();
        public ListCustomer()
        {
            InitializeComponent();
            defaultCustomer = CustomerBLL.getInstance().getAllCustomers();
            Reload();
        }
        public void LoadCustomers(List<User> users)
        {
            currentCustomer = users;
            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = users;

            dgvCustomers.Columns["Password"].Visible = false;
        }
        public void Reload()
        {
            this.LoadCustomers(defaultCustomer);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCustomer addForm = new AddCustomer(Reload);
            addForm.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 1)
            {
                EditCustomer editForm = new EditCustomer(Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells[0].Value), Reload);
                editForm.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 hàng để chỉnh sửa!");
                return;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                List<int> delUserIds = new List<int>();
                for (int i = 0; i <  dgvCustomers.SelectedRows.Count; i++)
                {
                    delUserIds.Add(Convert.ToInt32(dgvCustomers.SelectedRows[i].Cells[0].Value));
                }
                // goi bll
                CustomerBLL.getInstance().removeCustomer(delUserIds);
                Reload();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cbSort.SelectedItem.ToString();
            List<User> sortedCustomer = currentCustomer;
            switch (selected)
            {
                case "Tên tăng dần":
                    sortedCustomer = sortedCustomer.OrderBy(x => x.Name).ToList();
                    break;
                case "Tên giảm dần":
                    sortedCustomer = sortedCustomer.OrderByDescending(x => x.Name).ToList();
                    break;
                default: break;
            }
            LoadCustomers(sortedCustomer);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            List<User> listCustomer = CustomerBLL.getInstance().getCustomerByName(keyword);
            LoadCustomers(listCustomer);

        }
    }
}
