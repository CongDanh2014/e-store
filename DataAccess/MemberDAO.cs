using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataAccess
{
    public class MemberDAO
    {
        FStoreDBContext db = new FStoreDBContext();

        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }

        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public Member Login(string username, string password)
        {
            Member member = null;
            try
            {
                member = (from c in db.Members where c.Email == username && c.Password == password select c).FirstOrDefault();
                db.Entry(member).Collection(p => p.Orders).Load();
            }
            catch (Exception ex)
            {
                throw new Exception("Username or password not match. Please enter correct fields...");
            }
            return member;
        }
        public List<Member> GetMembers()
        {
            List<Member> members = null;
            try
            {
                members = db.Members.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return members;
        }

        public Member GetMemberByID(int id)
        {
            Member member = db.Members.Find(id);
            return member;
        }

        private void CheckValidateMember(Member member, bool isAllFieldsRequired = true, bool isDuplicateID = true, bool isDuplicateEmail = true, bool isEmailFormat = true)
        {
            if (isAllFieldsRequired)
            {
                if (member.Email == "" || member.Email == null || member.CompanyName == "" || member.CompanyName == null || member.City == "" || member.City == null || member.Country == "" || member.Country == null || member.Password == "" || member.Password == null)
                {
                    throw new Exception("All fields must be required.");
                }
            }

            if (isDuplicateID)
            {
                var checkID = (from c in GetMembers() where member.MemberId == c.MemberId select c).SingleOrDefault();
                if (checkID != null)
                {
                    throw new Exception("Duplicate member id, Please enter another id.");
                }
            }
            //check mail
            if (isDuplicateEmail)
            {
                var checkEmail = (from c in GetMembers() where member.Email == c.Email select c).SingleOrDefault();
                if (checkEmail != null)
                {
                    throw new Exception("Duplicate email, please enter another email.");
                }
            }


            if (isEmailFormat)
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(member.Email);
                if (!match.Success)
                {
                    throw new Exception("Not format email.");
                }
            }

        }
        public void CreateMember(Member member)
        {
            try
            {
                CheckValidateMember(member);
                db.Members.Add(member);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void DeleteMember(int id)
        {
            try
            {
                var item = db.Members.SingleOrDefault(c => c.MemberId == id);
                db.Members.Remove(item);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UpdateMember(Member member)
        {
            CheckValidateMember(member, true, false, false, true);
            var item = db.Members.Find(member.MemberId);
            if (item == null) throw new Exception("MemberId dosen't exist");
            item.Email = member.Email;
            item.CompanyName = member.CompanyName;
            item.City = member.City;
            item.Country = member.Country;
            item.Password = member.Password;
            db.SaveChanges();
        }
    }
}
