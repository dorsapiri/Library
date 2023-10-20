using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataAccess.ViewModel
{
    public interface IBorrowedBookDataAccess
    {
        bool Insert(BorrowedBook book);
        bool Update(BorrowedBook book);
        bool Delete(BorrowedBook book);
        void Save();
        BorrowedBook GetByBookId(int id);
        List<BorrowedBook> GetByMemberId(int id);
    }
}
