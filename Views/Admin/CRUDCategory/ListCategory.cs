using QLBS.BLL;
using QLBS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBS.Views.Admin.CRUDCategory
{
    public partial class ListCategory : Form
    {
        public ListCategory()
        {
            InitializeComponent();
            LoadCategory();
        }
        private void LoadCategory()
        {
            dgvCategory.DataSource = null;
            dgvCategory.DataSource = CategoryBLL.getInstance().getAllCategories();
            dgvCategory.Columns["CategoryId"].HeaderText = "Mã số";
            dgvCategory.Columns["Name"].HeaderText = "Tên danh mục";
            dgvCategory.Columns["Books"].Visible = false;
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
            Category newCategory = new Category();
            newCategory.Name = txtAddName.Text;
            CategoryBLL.getInstance().addCategory(newCategory);
            MessageBox.Show("Thêm danh mục mới thành công!");
            // thực hiện reload
            LoadCategory();
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
                CategoryBLL.getInstance().removeCategory(delCategories);
                BookBLL.getInstance().removeBookByCategory(delCategories);
                // thuc hien reload
                LoadCategory();
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
                Category newCategory = new Category();
                newCategory.Name = txtEditName.Text;
                int id = Convert.ToInt32(dgvCategory.SelectedCells[0].Value.ToString());
                CategoryBLL.getInstance().editCategory(id, newCategory);
                // thuc hien reload
                LoadCategory();
            }
            else
            {
                MessageBox.Show("Vui lòng chỉ chọn 1 danh mục để chỉnh sửa!");
                return;
            } 
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadCategory();
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
