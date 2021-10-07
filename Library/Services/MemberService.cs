using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class MemberService : IMember
    {
        private ApplicationDbContext _applicationDbContext;
        public MemberService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Member> AddMember(Member member)
        {
            await _applicationDbContext.Members.AddAsync(member);
            await _applicationDbContext.SaveChangesAsync();
            return member;
        }

        public async Task DeleteMember(Member member) 
        {
            _applicationDbContext.Members.Remove(member);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task EditMember(Member member) 
        {
            var existingMember = await _applicationDbContext.Members.FindAsync(member.Id);

            if(existingMember != null)
            {
                existingMember.FirstName = member.FirstName;
                existingMember.LastName = member.LastName;
                existingMember.Email = member.Email;

                _applicationDbContext.Members.Update(existingMember);
                await _applicationDbContext.SaveChangesAsync();
            }
        }

        public async Task<Member> GetMember(int id)
        {
            var member = await _applicationDbContext.Members.FindAsync(id);
            return member;
        }

        public async Task<Member> GetMemberByName(string firstName, string lastName)
        {
            var member = await _applicationDbContext.Members
                .Where(m => m.FirstName == firstName && m.LastName == lastName)
                .FirstOrDefaultAsync();

            return member;
        }

        public async Task<List<Member>> GetMembers()
        {
            var members = await _applicationDbContext.Members.ToListAsync();
            return members;
        }

        public async Task<List<Rental>> RentedBooks(int id)
        {
            var rentedBooks = await _applicationDbContext.RentedBooks
                .Include(r => r.Book)
                .Where(r => r.MemberId == id)
                .ToListAsync();

            return rentedBooks;
        }
    }
}
