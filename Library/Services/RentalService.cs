﻿using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        public Rental GetRent(int id)
        {
            var rental = _applicationDbContext.RentedBooks.Find(id);
            return rental;
        }

        public List<Rental> GetRents()
        {
            var rentals = _applicationDbContext.RentedBooks
                .Include(r => r.Book)
                .Include(r => r.Member)
                .ToList();

            return rentals;
        }

        public List<Rental> GetRentsByNameMember(string firtstName, string lastName)
        {
            var rentals = _applicationDbContext.RentedBooks
                .Include(r => r.Member)
                .Where(r => r.Member.FirstName == firtstName && r.Member.LastName == lastName)
                .ToList();

            return rentals;
        }

        public Rental RentBook(Rental rental)
        {
            _applicationDbContext.RentedBooks.Add(rental);
            _applicationDbContext.SaveChanges();

            var book = _bookService.GetBook(rental.BookId);
            var available = book.Available - 1;
            book.Available = available;
            _bookService.EditBook(book);

            return rental;
        }

        public void ReturnBook(Rental rental)
        {
            _applicationDbContext.RentedBooks.Remove(rental);
            _applicationDbContext.SaveChanges();

            var book = _bookService.GetBook(rental.BookId);
            var available = book.Available - 1;
            book.Available = available;
            _bookService.EditBook(book);
        }
    }
}