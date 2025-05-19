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
using QLBS.Migrations;
using System.IO;

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
            txtUrl.ReadOnly = true;
            string imagePath = Path.Combine(Application.StartupPath, editBook.ImageUrl.ToString());
            if (File.Exists(imagePath))
                prevPicture.Image = ImageHelper.GetInstance().ResizeImage(Image.FromFile(imagePath), new Size (100, 120));
            else
                prevPicture.Image = null;
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
                d();
                this.Close();
            }
            else
            {
                MessageBox.Show("Edit thất bại");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string picturesDir = Path.Combine(Application.StartupPath, "Pictures");
                    if (!Directory.Exists(picturesDir))
                        Directory.CreateDirectory(picturesDir);

                    string fileName = Path.GetFileName(ofd.FileName);
                    string destPath = Path.Combine(picturesDir, fileName);

                    if (!File.Exists(destPath))
                    {
                        File.Copy(ofd.FileName, destPath);
                    }

                    string relativePath = $"Pictures/{fileName}";
                    txtUrl.Text = relativePath;
                    prevPicture.Image = ImageHelper.GetInstance().ResizeImage(Image.FromFile(destPath), new Size(100, 120));
                }
            }
        }
    }
}
