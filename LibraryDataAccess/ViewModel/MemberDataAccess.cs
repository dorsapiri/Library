using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataAccess.ViewModel
{
    public class MemberDataAccess :IMemberDataAccess
    {
        LibraryEntities db;
        public MemberDataAccess(LibraryEntities libraryEntities) { 
            db = libraryEntities;
        }

        public bool Insert(Member newMember)
        {
            try
            {
                db.Members.Add(newMember);
                return true;
            }
            catch
            {
                return false;
            }
             
        }
        public bool Delete(Member member)
        {
            try
            {
                db.Entry(member).State = EntityState.Deleted;
                return true;
            }
            catch
            { return false; }
            
        }

        public List<Member> SelectAll()
        {
            return db.Members.ToList();
        }

        public bool Update(Member member)
        {
            try
            {
                db.Entry(member).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
            
            
        }
        public void Save()
        {
            db.SaveChanges();
        }

        public List<Member> SelectByName(string name)
        {
            return db.Members.Where(x => x.Name.Trim().ToLower().StartsWith(name)).ToList();
        }

       /* public List<MemberInfo> SelectCustomColumns()
        {
             
            return db.Members.Select(member => new MemberInfo { 
                Name = member.Name,
                RegistaryDate = member.RegistaryDate,
                Borrowed = ""
            }).ToList();
        }*/
    }
}
