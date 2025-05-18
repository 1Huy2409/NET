using QLBS.BLL;
using QLBS.DAL.Entities;
using QLBS.DTOs.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QLBS.Views.Admin.CRUDCategory
{
    public partial class ListCategory : Form
    {
        private List<CategoryDTO> currentList = new List<CategoryDTO>();
        public ListCategory()
        {
            InitializeComponent();
            Reload();
        }
        private void LoadCategory(List<CategoryDTO> list)
        {
            currentList = list;
            dgvCategory.Columns.Clear();

            dgvCategory.Columns.Add("Id", "Mã số");
            dgvCategory.Columns.Add("Name", "Tên danh mục");
            dgvCategory.Columns.Add("Description", "Mô tả");

            dgvCategory.Rows.Clear();
            foreach (var category in list)
            {
                dgvCategory.Rows.Add(category.Id, category.Name);
            }
        }
        private void Reload()
        {
            this.currentList = CategoryBLL.getInstance().getAllCategories();
            LoadCategory(currentList);
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtAddName.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập danh mục bạn muốn thêm!");
                return;
            }
            var categoryDTO = new CategoryCreateDTO
            {
                Name = txtAddName.Text.Trim()
            };

            if (CategoryBLL.getInstance().CreateCategory(categoryDTO))
            {
                MessageBox.Show("Thêm thể loại thành công!");
            }
            else
            {
                MessageBox.Show("Thêm thể loại thất bại! Có thể tên thể loại đã tồn tại.");
            }
            // thực hiện reload
            Reload();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvCategory.SelectedRows.Count > 0)
            {
                // thuc hien xoa
                List<int> delCategories = new List<int>();
                for (int i = 0; i <  dgvCategory.SelectedRows.Count; i++)
                {
                    delCategories.Add(Convert.ToInt32(dgvCategory.SelectedCells[0].Value.ToString()));
                }
                // goi bll
                CategoryBLL.getInstance().DeleteCategories(delCategories);
                BookBLL.getInstance().removeBookByCategory(delCategories);
                // thuc hien reload
                Reload();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa!");
                return;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCategory.SelectedRows.Count == 1)
            {
                // thực hiện edit
                int id = Convert.ToInt32(dgvCategory.SelectedCells[0].Value.ToString());
                var newCategory = new CategoryUpdateDTO
                {
                    Id = id,
                    Name = txtEditName.Text.Trim(),
                };
                if (CategoryBLL.getInstance().UpdateCategory(newCategory))
                {
                    MessageBox.Show("Edit thành công!");
                }
                else
                {
                    MessageBox.Show("Edit thất bại!");
                }

                // thuc hien reload
                Reload();
            }
            else
            {
                MessageBox.Show("Vui lòng chỉ chọn 1 danh mục để chỉnh sửa!");
                return;
            } 
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCategory_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategory.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvCategory.CurrentRow.Cells[0].Value.ToString());
                string name = dgvCategory.CurrentRow.Cells[1].Value.ToString();
                txtEditName.Text = name;
                txtID.Text = id.ToString();
            }
        }
    }
}
