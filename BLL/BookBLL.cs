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
        private readonly BookDAL _bookDAL;
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
            _bookDAL = new BookDAL();
        }
        // begin after dto
        public List<BookDTO> getAllBooks()
        {
            var books = _bookDAL.GetAllBooks();
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
            var book = _bookDAL.GetBookById(id);
            if (book == null) return null;

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
            var book = new Book
            {
                Title = bookDTO.Title,
                Author = bookDTO.Author,
                Price = bookDTO.Price,
                Stock = bookDTO.Stock,
                CategoryId = bookDTO.CategoryId,
                ImageUrl = bookDTO.ImageUrl
            };

            if (!_bookDAL.AddBook(book))
            {
                MessageBox.Show("Tên sách này đã tồn tại!");
                return false;
            }
            return true;
        }
        public bool UpdateBook(BookUpdateDTO bookDTO)
        {
            var book = _bookDAL.GetBookById(bookDTO.Id);
            if (book == null) return false;

            book.Title = bookDTO.Title;
            book.Author = bookDTO.Author;
            book.Price = bookDTO.Price;
            book.Stock = bookDTO.Stock;
            book.CategoryId = bookDTO.CategoryId;
            book.ImageUrl = bookDTO.ImageUrl;

            if (!_bookDAL.UpdateBook(book))
            {
                MessageBox.Show("Tên sách đã tồn tại!");
                return false;
            }
            return true;
        }
        public bool DeleteBooks(List<int> delBooks)
        {
            return _bookDAL.DeleteBooks(delBooks);
        }
        public List<BookDTO> SearchBooks(string keyword)
        {
            var books = _bookDAL.SearchBooks(keyword);
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
        public List<BookDTO> getBookByCategoryName(string categoryName)
        {
            var books = _bookDAL.GetBooksByCategory(categoryName);
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
      
        //
        public void removeBookByCategory(List<int> delCategories)
        {
            _bookDAL.RemoveBooksByCategory(delCategories);
        }
        // edit book
    }
}
