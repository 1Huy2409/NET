using QLBS.DAL;
using QLBS.DTOs.Book;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Forms;

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
        // begin after dto
        public List<BookDTO> getAllBooks()
        {
            var books = _context.Books.Include(b => b.Category).ToList();
            return books.Select(b => new BookDTO
            {
                Id = b.ID,
                Title = b.Title,
                Author = b.Author,
                Price = b.Price,
                Stock = b.Stock,
                CategoryId = b.CategoryId,
                CategoryName = b.Category.Name,
                ImageUrl = b.ImageUrl
            }).ToList();
        }
        public BookDTO GetBookById(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return null;
            }
            return new BookDTO
            {
                Id = book.ID,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                Stock = book.Stock,
                CategoryId = book.CategoryId,
                CategoryName = book.Category.Name,
                ImageUrl = book.ImageUrl
            };        
        }
        public bool CreateBook(BookCreateDTO bookDTO)
        {
            if (_context.Books.Any(b => b.Title == bookDTO.Title))
            {
                MessageBox.Show("Tên sách này đã tồn tại!");
                return false;
            }
            var book = new Book
            {
                Title = bookDTO.Title,
                Author = bookDTO.Author,
                Price = bookDTO.Price,
                Stock = bookDTO.Stock,
                CategoryId = bookDTO.CategoryId,
                ImageUrl = bookDTO.ImageUrl
            };

            _context.Books.Add(book);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateBook(BookUpdateDTO bookDTO)
        {
            var book = _context.Books.Find(bookDTO.Id);
            if (book == null)
            {
                return false;
            }
            if (_context.Books.Any(b => b.Title == bookDTO.Title && b.ID != bookDTO.Id))
            {
                MessageBox.Show("Tên danh mục đã tồn tại!");
                return false;
            }
            book.Title = bookDTO.Title;
            book.Author = bookDTO.Author;
            book.Price = bookDTO.Price;
            book.Stock = bookDTO.Stock;
            book.CategoryId = bookDTO.CategoryId;
            book.ImageUrl = bookDTO.ImageUrl;
            _context.SaveChanges();
            return true;
        }
        public bool DeleteBooks(List<int> delBooks)
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
            return true;
        }
        public List<BookDTO> SearchBooks(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return getAllBooks();
            }

            keyword = keyword.ToLower();
            return _context.Books
                .Include(b => b.Category)
                .Where(b => b.Title.ToLower().Contains(keyword) ||
                           b.Author.ToLower().Contains(keyword) ||
                           b.Category.Name.ToLower().Contains(keyword))
                .Select(b => new BookDTO
                {
                    Id = b.ID,
                    Title = b.Title,
                    Author = b.Author,
                    Price = b.Price,
                    Stock = b.Stock,
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.Name,
                    ImageUrl = b.ImageUrl
                })
                .ToList();
        }
        public List<BookDTO> getBookByCategoryName(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return getAllBooks();
            }

            return _context.Books
                .Include(b => b.Category)
                .Where(b => b.Category.Name.ToLower() == categoryName.ToLower())
                .Select(b => new BookDTO
                {
                    Id = b.ID,
                    Title = b.Title,
                    Author = b.Author,
                    Price = b.Price,
                    Stock = b.Stock,
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.Name,
                    ImageUrl = b.ImageUrl
                })
                .ToList();
        }
        public List<BookDTO> GetBooksByCategory(string name) 
        { 
            var listBooks = _context.Books
                           .Where(b => b.Category.Name == name).ToList();
            return listBooks.Select(b => new BookDTO
            {
                Id = b.ID,
                Title = b.Title,
                Author = b.Author,
                Price = b.Price,
                Stock = b.Stock,
                CategoryId = b.CategoryId,
                CategoryName = b.Category.Name,
                ImageUrl = b.ImageUrl
            }).ToList();
        }
        //
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
        // edit book
    }
}
