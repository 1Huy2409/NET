using QLBS.DAL;
using QLBS.Utils;
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
using QLBS.DTOs.Book;

namespace QLBS.Views.Customer.Buy
{
    public partial class Confirm : Form
    {
        private string imageBasePath = @"D:\HuyCoding\Winform\QLBS\bin\Debug\Pictures\";
        private BookDTO selectedBook { get; set; }
        public Confirm(BookDTO book)
        {
            this.selectedBook = book;
            InitializeComponent();
            LoadSelectedBook();
            RenderTotal();
        }
        private void LoadSelectedBook()
        {
            lbTitle.Text = selectedBook.Title;
            lbAuthor.Text = selectedBook.Author;
            lbPrice.Text = selectedBook.Price.ToString("N0") + "đ";
            lbCategory.Text = selectedBook.CategoryName;
            string imagePath = Path.Combine(imageBasePath, selectedBook.ImageUrl);
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
        private void RenderTotal()
        {
            decimal total = selectedBook.Price * numBook.Value;
            lbAmount.Text = total.ToString("N0") + "đ";
        }

        private void numBook_ValueChanged(object sender, EventArgs e)
        {
            numBook.Minimum = 1;
            numBook.Maximum = selectedBook.Stock;
            RenderTotal();
        }

        // button cancel
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if ((int)numBook.Value == 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng để thêm vào giỏ hàng!");
                return;
            }
            var existing = SessionManager.Cart.FirstOrDefault(c => c.Book.Id == selectedBook.Id);
            int quantityAdd = (int)numBook.Value;
            int currentInCart = existing != null ? existing.Quantity : 0;
            int totalAfterAdd = currentInCart + quantityAdd; 
            if (totalAfterAdd > selectedBook.Stock) 
            {
                MessageBox.Show("Vượt quá số lượng của sản phẩm trong kho!");
                return;
            }

            if (existing != null)
            {
                existing.Quantity = quantityAdd;
            }
            else
            {
                SessionManager.Cart.Add(new CartItem { Book =  selectedBook, Quantity = quantityAdd });
            }
            this.Close();
        }
    }
}
