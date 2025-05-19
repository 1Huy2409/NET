using QLBS.DAL;
using QLBS.Utils;
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
using QLBS.DTOs.Book;

namespace QLBS
{
    public partial class BookCard : UserControl
    {
        //private string imageBasePath = @"D:\HuyCoding\Winform\QLBS\bin\Debug\Pictures\";
        private string imageBasePath = @"D:\HuyCoding\Winform\QLBS\Pictures\";

        public BookDTO book { get; set; }
        public BookCard()
        {
            InitializeComponent();
        }
        public void UpdateData(BookDTO book)
        {
            this.book = book;
            lbTitle.Text = book.Title;
            lbAuthor.Text = "Tác giả: " + book.Author;
            lbPrice.Text = "Giá: " + book.Price.ToString("N0") + "đ";

            string imagePath = Path.Combine(System.Windows.Forms.Application.StartupPath, book.ImageUrl);

            try
            {
                // Giải phóng ảnh cũ nếu có
                if (imgBook.Image != null)
                {
                    imgBook.Image.Dispose();
                    imgBook.Image = null;
                }

                if (!string.IsNullOrEmpty(book.ImageUrl) && System.IO.File.Exists(imagePath))
                {
                    using (var img = Image.FromFile(imagePath))
                    {
                        imgBook.Image = ImageHelper.GetInstance().ResizeImage(img, new Size(120, 140));
                    }
                }
                else
                {
                    // Ảnh mặc định nếu không có ảnh
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
