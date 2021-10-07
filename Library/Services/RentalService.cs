using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class RentalService : IRental
    {
        private ApplicationDbContext _applicationDbContext;
        private IBook  _bookService;
        public RentalService(ApplicationDbContext applicationDbContext, IBook bookService)
        {
            _applicationDbContext = applicationDbContext;
            _bookService = bookService;
        }

        public async Task<Rental> GetRent(int id)
        {
            var rental = await _applicationDbContext.RentedBooks.FindAsync(id);
            return rental;
        }

        public async Task<List<Rental>> GetRents()
        {
            var rentals = await _applicationDbContext.RentedBooks
                .Include(r => r.Book)
                .Include(r => r.Member)
                .ToListAsync();

            return rentals;
        }

        public async Task<List<Rental>> GetRentsByNameMember(string firtstName, string lastName)
        {
            var rentals = await _applicationDbContext.RentedBooks
                .Include(r => r.Member)
                .Where(r => r.Member.FirstName == firtstName && r.Member.LastName == lastName)
                .ToListAsync();

            return rentals;
        }

        public async Task<Rental> RentBook(Rental rental)
        {
            await _applicationDbContext.RentedBooks.AddAsync(rental);
            await _applicationDbContext.SaveChangesAsync();

            var book = await _bookService.GetBook(rental.BookId);
            var available = book.Available - 1;
            book.Available = available;
            await _bookService.EditBook(book);

            return rental;
        }

        public async Task ReturnBook(Rental rental)
        {
            _applicationDbContext.RentedBooks.Remove(rental);
            await _applicationDbContext.SaveChangesAsync();

            var book = await _bookService.GetBook(rental.BookId);
            var available = book.Available - 1;
            book.Available = available;
            await _bookService.EditBook(book);
        }
    }
}