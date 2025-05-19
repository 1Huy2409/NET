using QLBS.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace QLBS.DAL
{
    public class CategoryDAL
    {
        private readonly QLBSDbContext _context;

        public CategoryDAL()
        {
            _context = new QLBSDbContext();
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.Find(id);
        }

        public bool AddCategory(Category category)
        {
            if (_context.Categories.Any(c => c.Name == category.Name))
            {
                return false;
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateCategory(Category category)
        {
            if (_context.Categories.Any(c => c.Name == category.Name && c.CategoryId != category.CategoryId))
            {
                return false;
            }
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCategories(List<int> categoryIds)
        {
            foreach (var id in categoryIds)
            {
                var category = _context.Categories.Find(id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                }
            }
            _context.SaveChanges();
            return true;
        }

        public List<string> GetAllCategoryNames()
        {
            return _context.Categories.Select(c => c.Name).ToList();
        }
    }
}