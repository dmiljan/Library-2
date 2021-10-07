using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Book> AddBook(Book book)
        {
            await _applicationDbContext.Books.AddAsync(book);
            await _applicationDbContext.SaveChangesAsync();
            return book;
        }

        public async Task DeleteBook(Book book)
        {
            _applicationDbContext.Books.Remove(book);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task EditBook(Book book)
        {
            var existingBook = await _applicationDbContext.Books.FindAsync(book.Id);
            if(existingBook != null)
            {
                existingBook.Name = book.Name;
                existingBook.Writer = book.Writer;
                existingBook.Year = book.Year;
                existingBook.PagesNumber = book.PagesNumber;
                existingBook.Available = book.Available;

                _applicationDbContext.Books.Update(existingBook);
                await _applicationDbContext.SaveChangesAsync();
            }
        }

        public async Task<Book> GetBook(int id)
        {
            var book = await _applicationDbContext.Books.FindAsync(id);
            return book;
        }

        public async Task<Book> GetBookByName(string name)
        {
            var book = await _applicationDbContext.Books
                .Where(b => b.Name == name)
                .FirstOrDefaultAsync();

            return book;
        }

        public async Task<List<Book>> GetBooks()
        {
            var books = await _applicationDbContext.Books.ToListAsync();
            return books;
        }
    }
}
