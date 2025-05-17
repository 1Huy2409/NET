    using QLBS.BLL;
using QLBS.DAL;
using QLBS.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBS.Views.Customer.History
{
    public partial class HistoryOrder : Form
    {
        public HistoryOrder()
        {
            InitializeComponent();
        }

        private void HistoryOrder_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }
        private void LoadOrders()
        {
            List<Order> list = OrderBLL.getInstance().GetOrdersByUserId(SessionManager.CurrentUser.ID);
            dgvOrders.Columns.Clear();
            dgvOrders.Columns.Add("ID", "Mã Đơn");
            dgvOrders.Columns.Add("UserName", "Khách hàng");
            dgvOrders.Columns.Add("OrderDate", "Ngày đặt");
            dgvOrders.Columns.Add("TotalPrice", "Tổng tiền");
            dgvOrders.Rows.Clear();
            foreach (var order in list)
            {
                dgvOrders.Rows.Add(order.ID, order.User.Name, order.OrderDate.ToString("dd/MM/yyyy"), order.TotalPrice.ToString("N0") + "đ");
            }
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOrders.CurrentRow != null)
            {
                int orderId = Convert.ToInt32(dgvOrders.CurrentRow.Cells[0].Value);
                var order = OrderBLL.getInstance().GetOrderById(orderId);
                if (order != null)
                {
                    dgvOrderDetails.Columns.Clear();
                    dgvOrderDetails.Columns.Add("Title", "Tên sách");
                    dgvOrderDetails.Columns.Add("price", "Giá");
                    dgvOrderDetails.Columns.Add("quantity", "Số lượng");
                    dgvOrderDetails.Columns.Add("Tổng giá", "Tổng giá");
                    dgvOrderDetails.Rows.Clear();
                    if (order.OrderDetails.Count > 0)
                    {
                        foreach (var detail in order.OrderDetails)
                        {
                            dgvOrderDetails.Rows.Add(
                                detail.Book.Title,
                                detail.price.ToString("N0"),
                                detail.quantity,
                                (detail.price * detail.quantity).ToString("N0")
                            );
                        }
                    }
                }
            }
        }
    }
}
