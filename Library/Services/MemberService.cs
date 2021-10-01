﻿using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        public Member GetMemberByName(string firstName, string lastName)
        {
            var member = _applicationDbContext.Members
                .Where(m => m.FirstName == firstName && m.LastName == lastName)
                .FirstOrDefault();

            return member;
        }

        public List<Member> GetMembers()
        {
            var members = _applicationDbContext.Members.ToList();
            return members;
        }

        public List<Rental> RentedBooks(int id)
        {
            var rentedBooks = _applicationDbContext.RentedBooks
                .Include(r => r.Book)
                .Where(r => r.MemberId == id)
                .ToList();

            return rentedBooks;
        }
    }
}
