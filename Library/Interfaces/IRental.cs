using Library.Models;
using System.Collections.Generic;

namespace Library.Interfaces
{
    public interface IRental
    {
        List<Rental> GetRents();
        List<Rental> GetRentsByNameMember(string firtstName, string lastName);
        Rental GetRent(int id);
        Rental RentBook(Rental rental);
        void ReturnBook(Rental rental);
        
    }
}
