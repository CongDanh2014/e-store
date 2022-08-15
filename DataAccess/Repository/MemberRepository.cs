using BussinessObject.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void CreateMember(Member member) => MemberDAO.Instance.CreateMember(member);

        public void DeleteMember(int id) => MemberDAO.Instance.DeleteMember(id);

        public Member GetMemberByID(int id) => MemberDAO.Instance.GetMemberByID(id);

        public IEnumerable<Member> GetMembers() => MemberDAO.Instance.GetMembers();

        public Member Login(string username, string password) => MemberDAO.Instance.Login(username, password);

        public void UpdateMember(Member member) => MemberDAO.Instance.UpdateMember(member);

    }
}
