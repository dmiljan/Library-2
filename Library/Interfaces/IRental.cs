using Library.Models;
using System.Collections.Generic;

namespace Library.Interfaces
{
    public interface IRental
    {
        List<Rental> GetRents();
        Rental GetRent(int id);
        Rental RentBook(Rental rental);
        void ReturnBook(Rental rental);
    }
}
