using QLBS.BLL;
using QLBS.DAL;
using QLBS.DTOs.Order;
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
    public partial class ConfirmOrder : Form
    {
        public ConfirmOrder()
        {
            InitializeComponent();
            LoadCart();
        }

        private void lbName_Click(object sender, EventArgs e)
        {

        }

        private void lbTotal_Click(object sender, EventArgs e)
        {

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
            lbName.Text = SessionManager.CurrentUser.Name;
            lbAddress.Text = SessionManager.CurrentUser.Address;
            lbPhone.Text = SessionManager.CurrentUser.Phone;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            // create new order
            OrderCreateDTO newOrder = new OrderCreateDTO
            {
                OrderDate = DateTime.Now,
                TotalAmount = Convert.ToDecimal(SessionManager.Cart.Sum(i => i.Book.Price * i.Quantity)),
                UserId = SessionManager.CurrentUser.ID
            };
            OrderBLL.getInstance().AddOrderWithDetails(newOrder, SessionManager.Cart);
            MessageBox.Show("Đặt hàng thành công!", "Thông báo");
            SessionManager.Cart.Clear();
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
