using Library.Interfaces;
using Library.Models;
using System;
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
        public Member AddMember(Member member)
        {
            _applicationDbContext.Members.Add(member);
            _applicationDbContext.SaveChanges();
            return member;
        }

        public void DeleteMember(Member member)
        {
            _applicationDbContext.Members.Remove(member);
            _applicationDbContext.SaveChanges();
        }

        public void EditMember(Member member)
        {
            var existingMember = _applicationDbContext.Members.Find(member.Id);

            if(existingMember != null)
            {
                existingMember.FirstName = member.FirstName;
                existingMember.LastName = member.LastName;
                existingMember.Email = member.Email;

                _applicationDbContext.Members.Update(existingMember);
                _applicationDbContext.SaveChanges();
            }
        }

        public Member GetMember(int id)
        {
            var member = _applicationDbContext.Members.Find(id);
            return member;
        }

        public List<Member> GetMembers()
        {
            var members = _applicationDbContext.Members.ToList();
            return members;
        }
    }
}
