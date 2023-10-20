using LibraryDataAccess.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataAccess.contex
{
    public class UnitOfWork : IDisposable
    {
        LibraryEntities db = new LibraryEntities();
        private IBookDataAccess _IBookDataAccess;
        private IMemberDataAccess _IMemberDataAccess;
        private IBorrowedBookDataAccess _IBorrowedBookDataAccess;
        public IBookDataAccess IBookDataAccess 
        { get 
            { 
                if (_IBookDataAccess == null)
                {
                    _IBookDataAccess = new BookDataAccess(db);
                }
                return _IBookDataAccess;
            } 
        }
        public IMemberDataAccess IMemberDataAccess
        {
            get
            {
                if(_IMemberDataAccess == null)
                {
                    _IMemberDataAccess = new MemberDataAccess(db);
                }
                return _IMemberDataAccess;
            }
        }
        public IBorrowedBookDataAccess IBorrowedBookDataAccess
        {
            get
            {
                if (_IBorrowedBookDataAccess == null)
                {
                    _IBorrowedBookDataAccess = new BorrowedBookDataAccess(db);
                }
                return _IBorrowedBookDataAccess;
            }
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
