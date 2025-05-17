using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBS.BLL;
using QLBS.DAL;
using QLBS.Utils;
using QLBS.Views.Admin.CRUDBook;

namespace QLBS.Views.Admin
{
    public partial class AddBook : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        public AddBook(MyDel d)
        {
            InitializeComponent();
            this.d = d;
            LoadCategory();
        }
        private void LoadCategory()
        {
            cbCategory.DataSource = CategoryBLL.getInstance().getAllCategories();
            cbCategory.SelectedIndex = -1;
            cbCategory.ValueMember = "CategoryId";
            cbCategory.DisplayMember = "Name";
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Kiểm tra định dạng trước khi tạo Book
            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                errorProvider1.SetError(txtPrice, "Giá tiền không hợp lệ!");
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock))
            {
                errorProvider1.SetError(txtStock, "Số lượng không hợp lệ!");
                return;
            }

            Book newBook = new Book
            {
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                CategoryId = (int)cbCategory.SelectedValue,
                Price = price,
                Stock = stock,
                ImageUrl = txtUrl.Text,
            };

            // Validate object sau khi chắc chắn không lỗi định dạng
            if (!ValidationHelper.Validate(newBook, this, errorProvider1))
            {
                Console.WriteLine("Đã vào validate.");
                return;
            }

            if (!BookBLL.getInstance().IsBookTitleExist(newBook.Title))
            {
                MessageBox.Show("Tên sách này đã tồn tại!");
                return;
            }

            BookBLL.getInstance().addBook(newBook);
            d(); // Hàm callback để reload
            this.Close();
        }

    }
}
