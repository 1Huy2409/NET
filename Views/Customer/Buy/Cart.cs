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

namespace QLBS.Views.Customer.Buy
{
    public partial class Cart : Form
    {
        public Cart()
        {
            InitializeComponent();
            LoadCart();
        }
        private void LoadCart()
        {
            dgvCart.Columns.Clear();
            dgvCart.Columns.Add("Title", "Tên sách");
            dgvCart.Columns.Add("Price", "Đơn giá");
            dgvCart.Columns.Add("Quantity", "Số lượng");
            dgvCart.Columns.Add("Total", "Thành tiền");
            dgvCart.Rows.Clear();
            foreach (var item in SessionManager.Cart)
            {
                dgvCart.Rows.Add(item.Book.Title, item.Book.Price, item.Quantity, item.Book.Price * item.Quantity);

            }
            lbTotal.Text = "Tổng tiền: " + SessionManager.Cart.Sum(i => i.Book.Price * i.Quantity).ToString("N0") + " đ";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            // open form xác nhận địa chỉ ,... rồi order
            ConfirmOrder confirmOrder = new ConfirmOrder();
            confirmOrder.Show();
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dgvCart.SelectedRows.Count; i++)
                {
                    var title = dgvCart.SelectedRows[i].Cells["Title"].Value.ToString();
                    var itemRemove = SessionManager.Cart.FirstOrDefault(c => c.Book.Title == title);
                    if (itemRemove != null)
                    {
                        SessionManager.Cart.Remove(itemRemove);
                        LoadCart();
                    }
                }
            }
        }
    }
}
