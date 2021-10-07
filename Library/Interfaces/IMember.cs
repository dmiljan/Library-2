using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Interfaces
{
    public interface IMember
    {
        Task<List<Member>> GetMembers();
        Task<Member> GetMember(int id);
        Task<Member> GetMemberByName(string firstName, string lastName);
        Task<Member> AddMember(Member member);
        Task DeleteMember(Member member); 
        Task EditMember(Member member); 
        Task<List<Rental>> RentedBooks(int id);
    }
}
