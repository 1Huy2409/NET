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

namespace QLBS.Views.Admin.CRUDOrder
{
    public partial class ListOrder : Form
    {
        private List<Order> currentOrders = new List<Order>();
        private List<Order> defaultOrders = new List<Order>();
        public ListOrder()
        {
            InitializeComponent();
            defaultOrders = OrderBLL.getInstance().GetAllOrders();
        }

        private void ListOrder_Load(object sender, EventArgs e)
        {
            LoadOrders(defaultOrders);
        }
        private void LoadOrders(List<Order> list)
        {
            currentOrders = list;
            dgvOrders.Columns.Clear();
            dgvOrders.Columns.Add("ID", "Mã Đơn");
            dgvOrders.Columns.Add("UserName", "Khách hàng");
            dgvOrders.Columns.Add("OrderDate", "Ngày đặt");
            dgvOrders.Columns.Add("TotalPrice", "Tổng tiền");
            dgvOrders.Rows.Clear();
            foreach (var order in list)
            {
                dgvOrders.Rows.Add(order.ID, order.User.Name, order.OrderDate.ToString("dd/MM/yyyy"),order.TotalPrice.ToString("N0") + "đ");
            }
        }
        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            List<Order> orders = OrderBLL.getInstance().GetOrderByUserName(keyword);
            LoadOrders(orders);
        }

        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cbSort.SelectedItem.ToString();
            List<Order> sortedList = currentOrders;
            switch(selected)
            {
                case "Mã đơn tăng dần":
                    sortedList = sortedList.OrderBy(o => o.ID).ToList();
                    break;
                case "Mã đơn giảm dần":
                    sortedList = sortedList.OrderByDescending(o => o.ID).ToList();
                    break;
                case "Ngày đặt mới nhất":
                    sortedList = sortedList.OrderByDescending(o => o.OrderDate).ToList();
                    break;
                case "Ngày đặt cũ nhất":
                    sortedList = sortedList.OrderBy(o => o.OrderDate).ToList();
                    break;
                case "Tổng tiền tăng dần":
                    sortedList = sortedList.OrderBy(o => o.TotalPrice).ToList();
                    break;
                case "Tổng tiền giảm dần":
                    sortedList = sortedList.OrderByDescending(o => o.TotalPrice).ToList();
                    break;
            }
            LoadOrders(sortedList);
        }
    }
}
