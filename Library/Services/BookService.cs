﻿using Library.Interfaces;
using Library.Models;
using System.Collections.Generic;
using System.Linq;

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
                existingBook.PagesNumber = book.PagesNumber;
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

        public Book GetBookByName(string name)
        {
            var book = _applicationDbContext.Books
                .Where(b => b.Name == name)
                .FirstOrDefault();

            return book;
        }

        public List<Book> GetBooks()
        {
            var books = _applicationDbContext.Books.ToList();
            return books;
        }
    }
}
