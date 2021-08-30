using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class BookService : IBook
    {
        private ApplicationDbContext _applicationDbContext;

        public BookService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Book AddBook(Book book)
        {
            _applicationDbContext.Books.Add(book);
            _applicationDbContext.SaveChanges();
            return book;
        }

        public void DeleteBook(Book book)
        {
            _applicationDbContext.Books.Remove(book);
            _applicationDbContext.SaveChanges();
        }

        public void EditBook(Book book)
        {
            var existingBook = _applicationDbContext.Books.Find(book.Id);
            if(existingBook != null)
            {
                existingBook.Name = book.Name;
                existingBook.Writer = book.Writer;
                existingBook.Year = book.Year;
                existingBook.Pages_number = book.Pages_number;
                existingBook.Available = book.Available;

                _applicationDbContext.Books.Update(existingBook);
                _applicationDbContext.SaveChanges();
            }
        }

        public Book GetBook(int id)
        {
            var book = _applicationDbContext.Books.Find(id);
            return book;
        }

        public List<Book> GetBooks()
        {
            var books = _applicationDbContext.Books.ToList();
            return books;
        }
    }
}
