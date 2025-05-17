using QLBS.BLL;
using QLBS.DAL;
using QLBS.Utils;
using QLBS.Views.Customer.Buy;
using QLBS.Views.Customer.History;
using QLBS.Views.Customer.Info;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBS.Views.Customer
{
    public partial class Dashboard : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        private List<Book> currentBooks = new List<Book>();
        public Dashboard(MyDel d)
        {
            this.d = d;
            InitializeComponent();
            LoadCategory();
            this.currentBooks = BookBLL.getInstance().getAllBooks();
            LoadBooksToFlowPanel(this.currentBooks);
        }
        private void LoadCategory()
        {
            cbCategory.Items.Add("All");
            List<string> categoriesName = CategoryBLL.getInstance().getAllCateName();
            cbCategory.Items.AddRange(categoriesName.ToArray());
        }
        private void LoadBooksToFlowPanel(List<Book> books)
        {

            flowBooks.Controls.Clear();

            foreach ( var book in books )
            {
                BookCard card = new BookCard();
                card.UpdateData(book);
                flowBooks.Controls.Add(card);    // giải thích chỗ này
            }
        }
        private void flowBooks_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            d();
            this.Close();
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            Cart cart = new Cart();
            cart.Show();
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            HistoryOrder order = new HistoryOrder();
            order.Show();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            CustomerInfo customerInfo = new CustomerInfo();
            customerInfo.Show();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // bắt sự kiện thay đổi lựa chọn trong combobox
            if (cbCategory.SelectedIndex >= 0)
            {
                if (cbCategory.SelectedItem.ToString() == "All")
                {
                    this.currentBooks = BookBLL.getInstance().getAllBooks();
                    LoadBooksToFlowPanel(this.currentBooks);
                }
                else
                {
                    string name = cbCategory.SelectedItem.ToString();
                    this.currentBooks = BookBLL.getInstance().getBooksByCategory(name);
                    if (this.currentBooks.Count > 0)
                    {
                        LoadBooksToFlowPanel(this.currentBooks);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm thuộc danh mục này!");
                    }
                }
            }
        }
    }
}
