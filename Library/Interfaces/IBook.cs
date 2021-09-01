using Library.Models;
using System.Collections.Generic;

namespace Library.Interfaces
{
    public interface IBook
    {
        List<Book> GetBooks();
        Book GetBook(int id);
        Book AddBook(Book book);
        void DeleteBook(Book book);
        void EditBook(Book book);
    }
}