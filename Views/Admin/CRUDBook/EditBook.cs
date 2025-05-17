using QLBS.BLL;
using QLBS.DAL;
using QLBS.Utils;
using QLBS.DTOs.Book;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace QLBS.Views.Admin.CRUDBook
{
    public partial class EditBook : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        private BookDTO editBook;
        private int id;
        public EditBook(int Id, MyDel d)
        {
            this.id = Id;
            this.d = d;
            InitializeComponent();
            editBook = BookBLL.getInstance().GetBookById(this.id);
            LoadCategory();
            LoadEditForm();
        }
        private void LoadCategory()
        {
            cbCategory.DataSource = CategoryBLL.getInstance().getAllCategories();
            cbCategory.ValueMember = "Id";
            cbCategory.DisplayMember = "Name";
        }
        private void LoadEditForm()
        {
            txtAuthor.Text = editBook.Author;
            txtTitle.Text = editBook.Title;
            cbCategory.SelectedValue = editBook.CategoryId;
            txtPrice.Text = editBook.Price.ToString();
            txtStock.Text = editBook.Stock.ToString();
            txtUrl.Text = editBook.ImageUrl.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // truyền book mới kèm id của nó
            var editBook = new BookUpdateDTO
            {
                Id = this.id,
                Title = txtTitle.Text.Trim(),
                Author = txtAuthor.Text.Trim(),
                Price = Convert.ToDecimal(txtPrice.Text),
                Stock = Convert.ToInt32(txtStock.Text),
                CategoryId = (int)cbCategory.SelectedValue,
                ImageUrl = txtUrl.Text.Trim()
            };
            if (!ValidationHelper.Validate(editBook, this, errorProvider1))
            {
                return;
            }
            bool checkEdit = BookBLL.getInstance().UpdateBook(editBook);
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
