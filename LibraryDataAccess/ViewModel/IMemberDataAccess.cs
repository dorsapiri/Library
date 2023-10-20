using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataAccess.ViewModel
{
    public interface IMemberDataAccess
    {
        bool Insert(Member newMember);
        bool Delete(Member member);
        List<Member> SelectAll();
        bool Update(Member member);
        List<Member> SelectByName(string name);
        void Save();
    }
}
