using QLBS.BLL;
using QLBS.DAL;
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

namespace QLBS.Views.Admin.CRUDCustomer
{
    public partial class ListCustomer : Form
    {
        private List<CustomerDTO> defaultCustomer = new List<CustomerDTO>();
        private List<CustomerDTO> currentCustomer = new List<CustomerDTO>();
        public ListCustomer()
        {
            InitializeComponent();
            Reload();
        }
        public void LoadCustomers(List<CustomerDTO> customers)
        {
            currentCustomer = customers;
            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = customers;
        }
        public void Reload()
        {
            defaultCustomer = CustomerBLL.getInstance().getAllCustomers();
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
                if (CustomerBLL.getInstance().DeleteCustomers(delUserIds))
                {
                    MessageBox.Show("Xóa thành công!");
                }
                else
                {
                    MessageBox.Show("Xóa thất bại");
                }
                Reload();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 khách hàng để xóa!");
                return;
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cbSort.SelectedItem.ToString();
            List<CustomerDTO> sortedCustomer = currentCustomer;
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
            string keyword = txtSearch.Text.Trim();
            var searchResults = CustomerBLL.getInstance().SearchCustomers(keyword);
            LoadCustomers(searchResults);
        }
    }
}
