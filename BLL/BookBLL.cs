using QLBS.DAL;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace QLBS.BLL
{
    public class BookBLL
    {
        private QLBSDbContext _context;
        private static BookBLL _instance;
        public static BookBLL getInstance()
        {
            if (_instance == null)
            {
                _instance = new BookBLL();
            }
            return _instance;
        }
        public BookBLL()
        {
            _context = new QLBSDbContext();
        }
        public List<Book> getAllBooks()
        {
            return _context.Books.Include(b => b.Category)
                                 .ToList();
        }
        public List<object> getDisplayBook(List<Book> books)
        {
            return books.Select(b => new
            {
                b.ID,
                b.Title,
                b.Author,
                b.Stock,
                b.Price,
                b.ImageUrl,
                b.CategoryId,
                Category = b.Category?.Name
            }).Cast<object>().ToList();
            
        }
        public List<Book> getBooksByCategory(string name) 
        { 
            return _context.Books
                           .Where(b => b.Category.Name == name).ToList();
        }
        public void addBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
        public void removeBook(List<int> delBooks)
        {
            for (int i = 0; i < delBooks.Count; i++)
            {
                int id = delBooks[i];
                var delBook = _context.Books.Find(id);
                if (delBook != null)
                {
                    _context.Books.Remove(delBook);
                    _context.SaveChanges();
                }
            }
        }
        public void removeBookByCategory(List<int> delCategories)
        {
            for (int i = 0; i < delCategories.Count;i++)
            {
                int id = delCategories[i];
                var delBook = _context.Books.Where(b => b.CategoryId == id).FirstOrDefault();
                if (delBook != null)
                {
                    _context.Books.Remove(delBook);
                    _context.SaveChanges();
                }
            }    
        }
        public Book getBookById(int id)
        {
            return _context.Books.FirstOrDefault(book => book.ID == id);
        }
        public List<Book> getBookByTitle(string title)
        {
            return _context.Books
                        .Where(book => book.Title.ToLower().Contains(title))
                        .ToList();
        }
        // edit book
        public bool editBook(int id, Book book)
        {
            var findBook = _context.Books.Find(id);
            if (findBook != null)
            {
                findBook.ID = id;
                findBook.Title = book.Title;
                findBook.CategoryId = book.CategoryId;
                findBook.Author = book.Author;
                findBook.Price = book.Price;
                findBook.Stock = book.Stock;
                findBook.ImageUrl = book.ImageUrl;
                _context.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }
        public bool IsBookTitleExist(string title, int? id = null)
        {
            var book = _context.Books.FirstOrDefault(b => b.Title == title && b.ID != (id ?? -1));
            if (book != null)
            {
                return false;
            }
            return true;
        }
    }
}
