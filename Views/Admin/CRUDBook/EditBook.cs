using QLBS.BLL;
using QLBS.DAL;
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

namespace QLBS.Views.Admin.CRUDBook
{
    public partial class EditBook : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        private Book editBook;
        private int id;
        public EditBook(int Id, MyDel d)
        {
            this.id = Id;
            this.d = d;
            InitializeComponent();
            editBook = BookBLL.getInstance().getBookById(Id);
            LoadCategory();
            LoadEditForm(editBook);
        }
        private void LoadCategory()
        {
            cbCategory.DataSource = CategoryBLL.getInstance().getAllCategories();
            cbCategory.ValueMember = "CategoryId";
            cbCategory.DisplayMember = "Name";
        }
        private void LoadEditForm(Book book)
        {
            txtAuthor.Text = book.Author;
            txtTitle.Text = book.Title;
            cbCategory.SelectedValue = book.CategoryId;
            txtPrice.Text = book.Price.ToString();
            txtStock.Text = book.Stock.ToString();
            txtUrl.Text = book.ImageUrl.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // truyền book mới kèm id của nó
            Book editBook = new Book
            {
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                CategoryId = (int)cbCategory.SelectedValue,
                Price = Convert.ToDecimal(txtPrice.Text),
                Stock = Convert.ToInt32(txtStock.Text),
                ImageUrl = txtUrl.Text
            };
            if (!ValidationHelper.Validate(editBook, this, errorProvider1))
            {
                return;
            }
            if (!BookBLL.getInstance().IsBookTitleExist(editBook.Title, id))
            {
                MessageBox.Show("Tên sách này đã tồn tại!");
                return;
            }
            bool checkEdit = BookBLL.getInstance().editBook(id, editBook);
            if (checkEdit)
            {
                MessageBox.Show("Edit thành công");
                this.Close();
            }
            else
            {
                MessageBox.Show("Edit thất bại");
            }
            d();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
