using QLBS.DAL;
using QLBS.Views.Customer.Buy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace QLBS
{
    public partial class BookCard : UserControl
    {
        private string imageBasePath = @"D:\HuyCoding\Winform\QLBS\bin\Debug\Pictures\";
        public Book book { get; set; }
        public BookCard()
        {
            InitializeComponent();
        }
        public void UpdateData(Book book)
        {
            this.book = book;
            lbTitle.Text = book.Title;
            lbAuthor.Text = "Tác giả: " + book.Author;
            lbPrice.Text = "Giá: " + book.Price.ToString("N0") + "đ";
            string imagePath = Path.Combine(imageBasePath, book.ImageUrl);
            try
            {
                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                {
                    imgBook.Image = Image.FromFile(imagePath);
                }
                else
                {
                    imgBook.Image = null;
                }
            }
            catch
            {
                imgBook.Image = null;
            }
        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            Confirm confirmForm = new Confirm(this.book);
            confirmForm.ShowDialog();
        }
    }
}
