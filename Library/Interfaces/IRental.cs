using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Interfaces
{
    public interface IRental
    {
        Task<List<Rental>> GetRents();
        Task<List<Rental>> GetRentsByNameMember(string firtstName, string lastName);
        Task<Rental> GetRent(int id);
        Task<Rental> RentBook(Rental rental);
        Task ReturnBook(Rental rental);
        
    }
}
