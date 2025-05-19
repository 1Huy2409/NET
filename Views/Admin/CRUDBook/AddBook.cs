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
using QLBS.BLL;
using QLBS.DAL;
using QLBS.DTOs.Book;
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
            txtUrl.ReadOnly = true;
        }
        private void LoadCategory()
        {
            cbCategory.DataSource = CategoryBLL.getInstance().getAllCategories();
            cbCategory.SelectedIndex = -1;
            cbCategory.ValueMember = "Id";
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

            var newBook = new BookCreateDTO
            {
                Title = txtTitle.Text.Trim(),
                Author = txtAuthor.Text.Trim(),
                Price = price,
                Stock = stock,
                CategoryId = (int)cbCategory.SelectedValue,
                ImageUrl = txtUrl.Text.Trim()
            };

            // Validate object sau khi chắc chắn không lỗi định dạng
            if (!ValidationHelper.Validate(newBook, this, errorProvider1))
            {
                return;
            }

            if (BookBLL.getInstance().CreateBook(newBook))
            {
                MessageBox.Show("Thêm sách thành công!");
                d();
                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm sách thất bại!");
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string picturesDir = Path.Combine(Application.StartupPath, "Pictures");
                    if (!Directory.Exists(picturesDir))
                        Directory.CreateDirectory(picturesDir);

                    string fileName = Path.GetFileName(ofd.FileName);
                    string destPath = Path.Combine(picturesDir, fileName);

                    // Nếu file nguồn và file đích là cùng một file thì không copy lại
                    if (!string.Equals(ofd.FileName, destPath, StringComparison.OrdinalIgnoreCase))
                    {
                        // Nếu file đích đã tồn tại, đổi tên file mới
                        if (!File.Exists(destPath))
                        {
                            File.Copy(ofd.FileName, destPath);
                        }
                    }

                    // Lưu đường dẫn tương đối vào textbox
                    string relativePath = $"Pictures/{fileName}";
                    txtUrl.Text = relativePath;

                    // Hiển thị ảnh preview
                    prevPicture.Image = ImageHelper.GetInstance().ResizeImage(Image.FromFile(destPath), new Size(100, 120));
                }
            }
        }
    }
}
