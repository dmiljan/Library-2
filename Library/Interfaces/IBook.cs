using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
