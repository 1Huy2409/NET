using QLBS.BLL;
using QLBS.DAL;
using QLBS.DTOs.Book;
using QLBS.Utils;
using QLBS.Views.Admin.CRUDBook;
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
namespace QLBS.Views.Admin
{
    public partial class ListBooks : Form
    {
        //private string imageBasePath = @"D:\HuyCoding\Winform\QLBS\bin\Debug\Pictures\";
        private string imageBasePath = @"D:\HuyCoding\Winform\QLBS\Pictures\";

        private List<BookDTO> defaultBooks = new List<BookDTO>();
        private List<BookDTO> currentBooks = new List<BookDTO>();
        public ListBooks()
        {
            InitializeComponent();
            ConfigureDataGridView();
            LoadCategory();
            dgvBooks.CellFormatting += DgvBooks_CellFormatting;
            Reload();
        }

        private void ConfigureDataGridView()
        {
            dgvBooks.RowTemplate.Height = 100;
        }

        private void LoadCategory()
        {
            cbCategory.Items.Add("All");
            List<string> categoriesName = CategoryBLL.getInstance().getAllCateName();
            cbCategory.Items.AddRange(categoriesName.ToArray());
        }
        private void LoadBooks(List<BookDTO> books)
        {
               
            currentBooks = books;
            dgvBooks.DataSource = null;
            dgvBooks.DataSource = books;

            // Đặt tên cột
            dgvBooks.Columns["Id"].HeaderText = "ID";
            dgvBooks.Columns["Title"].HeaderText = "Tên sách";
            dgvBooks.Columns["Author"].HeaderText = "Tác giả";
            dgvBooks.Columns["CategoryName"].HeaderText = "Danh mục";
            dgvBooks.Columns["Price"].HeaderText = "Giá tiền";
            dgvBooks.Columns["Stock"].HeaderText = "Số lượng";

            dgvBooks.Columns["ImageUrl"].Visible = false;
            dgvBooks.Columns["CategoryId"].Visible = false;
            // Thêm cột ảnh nếu chưa có
            if (!dgvBooks.Columns.Contains("BookImage"))
            {
                DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
                imageColumn.HeaderText = "Ảnh sách";
                imageColumn.Name = "BookImage";
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dgvBooks.Columns.Add(imageColumn);
            }
            dgvBooks.Columns["BookImage"].DisplayIndex = dgvBooks.Columns.Count - 1;
        }
        public void Reload()
        {
            defaultBooks = BookBLL.getInstance().getAllBooks();
            this.LoadBooks(defaultBooks);
        }
        private void DgvBooks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Chỉ xử lý cho cột ảnh nếu tồn tại đầy đủ điều kiện
            if (dgvBooks.Columns.Contains("BookImage") &&
                e.ColumnIndex >= 0 &&
                dgvBooks.Columns[e.ColumnIndex].Name == "BookImage" &&
                e.RowIndex >= 0 &&
                dgvBooks.Columns.Contains("ImageUrl"))
            {
                var cellValue = dgvBooks.Rows[e.RowIndex].Cells["ImageUrl"].Value;

                if (cellValue != null)
                {
                    string imageName = cellValue.ToString();
                    if (!string.IsNullOrEmpty(imageName))
                    {
                        string fullPath = Path.Combine(imageBasePath, imageName);
                        if (File.Exists(fullPath))
                        {
                            try
                            {
                                Image img = Image.FromFile(fullPath);
                                e.Value = ImageHelper.GetInstance().ResizeImage(img, new Size(80, 100));
                            }
                            catch
                            {
                                e.Value = null; // hoặc ảnh mặc định
                            }
                        }
                        else
                        {
                            e.Value = null;
                        }
                    }
                    else
                    {
                        e.Value = null;
                    }
                }
                else
                {
                    e.Value = null;
                }
            }
            if (dgvBooks.Columns[e.ColumnIndex].Name == "Price" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal price))
                {
                    e.Value = price.ToString("N0") + "đ";
                    e.FormattingApplied = true; // đã áp dụng format rồi
                }
            }
        }


        //private Image ResizeImage(Image imgToResize, Size size)
        //{
        //    var resized = new Bitmap(imgToResize, size);
        //    return resized;
        //}

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddBook AddForm = new AddBook(Reload);
            AddForm.Show();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            List<int> delBooks = new List<int>();
            if (dgvBooks.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dgvBooks.SelectedRows.Count; i++)
                {
                    delBooks.Add(Convert.ToInt32(dgvBooks.SelectedRows[i].Cells["Id"].Value));
                }
            }
            // truyền list ID cần xóa này vào BookBLL 
            BookBLL.getInstance().DeleteBooks(delBooks);
            Reload();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 1)
            {
                Console.WriteLine("ID là: " + dgvBooks.SelectedRows[0].Cells["Id"].Value.ToString());

                // thuc hien edit
                EditBook editForm = new EditBook(Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["Id"].Value), Reload);
                editForm.Show();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            var searchResults = BookBLL.getInstance().SearchBooks(keyword);
            LoadBooks(searchResults);
        }

        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cbSort.SelectedItem.ToString();
            List<BookDTO> sortedBook = currentBooks;
            switch (selected)
            {
                case "Tiêu đề tăng dần":
                    sortedBook = currentBooks.OrderBy(x => x.Title).ToList();
                    break;
                case "Tiêu đề giảm dần":
                    sortedBook = currentBooks.OrderByDescending(x => x.Title).ToList();
                    break;
                case "Giá tăng dần":
                    sortedBook = currentBooks.OrderBy(x => x.Price).ToList();
                    break;
                case "Giá giảm dần":
                    sortedBook = currentBooks.OrderByDescending(x => x.Price).ToList();
                    break;
                default:
                    break;
            }
            LoadBooks(sortedBook);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // load sách theo tên của danh mục
            if (cbCategory.SelectedIndex >= 0)
            {
                if (cbCategory.SelectedItem.ToString() == "All")
                {
                    Reload();
                }
                else
                {
                    string name = cbCategory.SelectedItem.ToString();
                    List<BookDTO> booksByCategory = BookBLL.getInstance().GetBooksByCategory(name);
                    if (booksByCategory.Count > 0)
                    {
                        LoadBooks(booksByCategory);
                    }
                    else
                    {
                        LoadBooks(defaultBooks);
                    }
                }
            }
        }
    }
}
