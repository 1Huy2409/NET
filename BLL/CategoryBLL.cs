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
        private QLBSDbContext _context;
        private static CategoryBLL _instance;
        public CategoryBLL()
        {
            _context = new QLBSDbContext();
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
            return _context.Categories
            .Select(c => new CategoryDTO
            {
                Id = c.CategoryId,
                Name = c.Name
            })
            .ToList();
        }
        // get by id
        public CategoryDTO GetCategoryById(int id)
        {
            return _context.Categories
                .Where(c => c.CategoryId == id)
                .Select(c => new CategoryDTO
                {
                    Id = c.CategoryId,
                    Name = c.Name
                })
                .FirstOrDefault();
        }
        public bool CreateCategory(CategoryCreateDTO categoryDTO)
        {
            if (_context.Categories.Any(c => c.Name == categoryDTO.Name))
            {
                MessageBox.Show("Tên danh mục đã tồn tại!");
                return false;
            }
            var category = new Category
            {
                Name = categoryDTO.Name
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateCategory(CategoryUpdateDTO categoryDTO)
        {
            var category = _context.Categories.Find(categoryDTO.Id);
            if (_context.Categories.Any(c => c.Name == categoryDTO.Name && c.CategoryId != categoryDTO.Id))
            {
                MessageBox.Show("Tên danh mục đã tồn tại!");
                return false;
            }
            category.Name = categoryDTO.Name;
            _context.SaveChanges();
            return true;
        }
        public bool DeleteCategories(List<int> delCategories)
        {
            for (int i = 0; i < delCategories.Count; i++)
            {
                int id = delCategories[i];
                var delCategory = _context.Categories.Where(cate => cate.CategoryId == id).FirstOrDefault();
                if (delCategory != null)
                {
                    _context.Categories.Remove(delCategory);
                    _context.SaveChanges();
                }
            }
            return true;
        }
        public List<string> getAllCateName()
        {
            return _context.Categories.Select(c => c.Name).ToList();
        }
        // end
    }
}
