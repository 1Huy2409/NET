using QLBS.BLL;
using QLBS.DAL;
using QLBS.DTOs.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBS.Views.Admin.CRUDOrder
{
    public partial class ListOrder : Form
    {
        private List<OrderDTO> currentOrders = new List<OrderDTO>();
        private List<OrderDTO> defaultOrders = new List<OrderDTO>();
        public ListOrder()
        {
            InitializeComponent();
            defaultOrders = OrderBLL.getInstance().GetAllOrders();
        }

        private void ListOrder_Load(object sender, EventArgs e)
        {
            LoadOrders(defaultOrders);
        }
        private void LoadOrders(List<OrderDTO> list)
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
                dgvOrders.Rows.Add(order.Id, order.UserName, order.OrderDate.ToString("dd/MM/yyyy"),order.TotalAmount.ToString("N0") + "đ");
            }
        }
        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow != null)
            {
                int orderId = Convert.ToInt32(dgvOrders.CurrentRow.Cells["ID"].Value);
                var orderDetails = OrderDetailBLL.getInstance().GetOrderDetails(orderId);
                if (orderDetails != null)
                {
                    dgvOrderDetails.Columns.Clear();
                    dgvOrderDetails.Columns.Add("Title", "Tên sách");
                    dgvOrderDetails.Columns.Add("price", "Giá");
                    dgvOrderDetails.Columns.Add("quantity", "Số lượng");
                    dgvOrderDetails.Columns.Add("Tổng giá", "Tổng giá");
                    dgvOrderDetails.Rows.Clear();
                    foreach (var detail in orderDetails)
                    {
                        dgvOrderDetails.Rows.Add(
                            detail.BookTitle,
                            detail.Price.ToString("N0"),
                            detail.Quantity,
                            detail.Subtotal.ToString("N0")
                        );
                    }
                }    
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            List<OrderDTO> orders = OrderBLL.getInstance().GetOrderByUserName(keyword);
            LoadOrders(orders);
        }

        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cbSort.SelectedItem.ToString();
            List<OrderDTO> sortedList = currentOrders;
            switch(selected)
            {
                case "Mã đơn tăng dần":
                    sortedList = sortedList.OrderBy(o => o.Id).ToList();
                    break;
                case "Mã đơn giảm dần":
                    sortedList = sortedList.OrderByDescending(o => o.Id).ToList();
                    break;
                case "Ngày đặt mới nhất":
                    sortedList = sortedList.OrderByDescending(o => o.OrderDate).ToList();
                    break;
                case "Ngày đặt cũ nhất":
                    sortedList = sortedList.OrderBy(o => o.OrderDate).ToList();
                    break;
                case "Tổng tiền tăng dần":
                    sortedList = sortedList.OrderBy(o => o.TotalAmount).ToList();
                    break;
                case "Tổng tiền giảm dần":
                    sortedList = sortedList.OrderByDescending(o => o.TotalAmount).ToList();
                    break;
            }
            LoadOrders(sortedList);
        }
    }
}
