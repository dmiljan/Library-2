using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Interfaces
{
    public interface IBook
    {
        Task<List<Book>> GetBooks();
        Task<Book> GetBook(int id);
        Task<Book> GetBookByName(string name);
        Task<Book> AddBook(Book book);
        Task DeleteBook(Book book);
        Task EditBook(Book book);
    }
}