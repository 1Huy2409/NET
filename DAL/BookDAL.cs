using QLBS.DAL.Entities;
using QLBS.DTOs.Book;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace QLBS.DAL
{
    public class BookDAL
    {
        private readonly QLBSDbContext _context;

        public BookDAL()
        {
            _context = new QLBSDbContext();
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.Include(b => b.Category).ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.Find(id);
        }

        public bool AddBook(Book book)
        {
            if (_context.Books.Any(b => b.Title == book.Title))
            {
                return false;
            }
            _context.Books.Add(book);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateBook(Book book)
        {
            if (_context.Books.Any(b => b.Title == book.Title && b.ID != book.ID))
            {
                return false;
            }
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteBooks(List<int> bookIds)
        {
            foreach (var id in bookIds)
            {
                var book = _context.Books.Find(id);
                if (book != null)
                {
                    _context.Books.Remove(book);
                }
            }
            _context.SaveChanges();
            return true;
        }

        public List<Book> SearchBooks(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return GetAllBooks();
            }

            keyword = keyword.ToLower();
            return _context.Books
                .Include(b => b.Category)
                .Where(b => b.Title.ToLower().Contains(keyword) ||
                           b.Author.ToLower().Contains(keyword) ||
                           b.Category.Name.ToLower().Contains(keyword))
                .ToList();
        }

        public List<Book> GetBooksByCategory(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return GetAllBooks();
            }

            return _context.Books
                .Include(b => b.Category)
                .Where(b => b.Category.Name.ToLower() == categoryName.ToLower())
                .ToList();
        }

        public void RemoveBooksByCategory(List<int> categoryIds)
        {
            foreach (var id in categoryIds)
            {
                var books = _context.Books.Where(b => b.CategoryId == id).ToList();
                _context.Books.RemoveRange(books);
            }
            _context.SaveChanges();
        }
    }
}