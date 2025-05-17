using QLBS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

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
        public List<Category> getAllCategories()
        {
            return _context.Categories.ToList();
        }
        public List<string> getAllCateName()
        {
            return _context.Categories.Select(c => c.Name).ToList();
        }
        public void addCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public bool editCategory(int id, Category category)
        {
            var findCategory = _context.Categories.Where(cate => cate.CategoryId == id).FirstOrDefault();
            if ( findCategory != null )
            {
                findCategory.Name = category.Name;
                findCategory.CategoryId = category.CategoryId;
            }
            return true;
        }
        public void removeCategory (List<int> delCategories)
        {
            for (int i = 0; i < delCategories.Count; i++)
            {
                int id = delCategories[i];
                var delCategory = _context.Categories.Where(cate => cate.CategoryId == id).FirstOrDefault();
                if ( delCategory != null )
                {
                    _context.Categories.Remove(delCategory);
                    _context.SaveChanges();
                }
            }
        }
    }
}
