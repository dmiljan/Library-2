using Library.Models;
using System.Collections.Generic;

namespace Library.Interfaces
{
    public interface IMember
    {
        List<Member> GetMembers();
        Member GetMember(int id);
        Member AddMember(Member member);
        void DeleteMember(Member member);
        void EditMember(Member member);
    }
}
