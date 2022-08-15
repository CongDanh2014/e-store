using BussinessObject.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        Member Login(string username, string password);
        void CreateMember(Member member);

        Member GetMemberByID(int id);
        IEnumerable<Member> GetMembers();
        void UpdateMember(Member member);
        void DeleteMember(int id);
    }
}
