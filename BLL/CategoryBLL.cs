using QLBS.DAL;
using QLBS.DAL.Entities;
using QLBS.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBS.BLL
{
    public class CategoryBLL
    {
        private readonly CategoryDAL _categoryDAL;
        private static CategoryBLL _instance;
        public CategoryBLL()
        {
            _categoryDAL = new CategoryDAL();
        }
        public static CategoryBLL getInstance()
        {
            if ( _instance == null )
            {
                _instance = new CategoryBLL();
            }
            return _instance;
        }
        // method get all categories
        public List<CategoryDTO> getAllCategories()
        {
            var categories = _categoryDAL.GetAllCategories();
            return categories.Select(c => new CategoryDTO
            {
                Id = c.CategoryId,
                Name = c.Name
            }).ToList();
        }
        // get by id
        public bool CreateCategory(CategoryCreateDTO categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name
            };

            if (!_categoryDAL.AddCategory(category))
            {
                MessageBox.Show("Tên danh mục đã tồn tại!");
                return false;
            }
            return true;
        }
        public bool UpdateCategory(CategoryUpdateDTO categoryDTO)
        {
            var category = _categoryDAL.GetCategoryById(categoryDTO.Id);
            if (category == null) return false;

            category.Name = categoryDTO.Name;

            if (!_categoryDAL.UpdateCategory(category))
            {
                MessageBox.Show("Tên danh mục đã tồn tại!");
                return false;
            }
            return true;
        }
        public bool DeleteCategories(List<int> delCategories)
        {
            return _categoryDAL.DeleteCategories(delCategories);
        }
        public List<string> getAllCateName()
        {
            return _categoryDAL.GetAllCategoryNames();
        }
        // end
    }
}
