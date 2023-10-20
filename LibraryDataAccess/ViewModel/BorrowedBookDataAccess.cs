using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LibraryDataAccess.ViewModel
{
    public class BorrowedBookDataAccess : IBorrowedBookDataAccess
    {
        LibraryEntities db;
        public BorrowedBookDataAccess(LibraryEntities libraryEntities)
        {
            db = libraryEntities;
        }

        public bool Delete(BorrowedBook book)
        {
            try
            {
                db.Entry(book).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public BorrowedBook GetByBookId(int id)
        {
            return db.BorrowedBooks.First(x => x.BookId == id);
        }

        public List<BorrowedBook> GetByMemberId(int id)
        {
            return db.BorrowedBooks.Where(x => x.MemberId == id).ToList();
        }

        public bool Insert(BorrowedBook book)
        {
            try
            {
                db.BorrowedBooks.Add(book);
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

        public bool Update(BorrowedBook book)
        {
            try
            {
                db.Entry(book).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
