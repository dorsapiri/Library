using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;





namespace LibraryDataAccess.ViewModel
{

    public class BookDataAccess : IBookDataAccess
    {

        LibraryEntities db;

        public BookDataAccess(LibraryEntities libraryEntities)
        {
            db = libraryEntities;
        }

        public bool Insert(Book newBook)
        {
            try
            {
                db.Books.Add(newBook);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Book book)
        {
            try
            {
                db.Entry(book).State = EntityState.Deleted;
                return true;
            }
            catch { return false; }

        }


        public List<Book> SelectAll()
        {
            return db.Books.ToList();
        }

        public bool Update(Book book)
        {
            try
            {
                db.Entry(book).State = EntityState.Modified;
                return true;
            }
            catch { return false; }
        }
        public void Save()
        {
            db.SaveChanges();
        }

        public List<Book> SelectByName(string name)
        {
            return db.Books.Where(x => x.Name.Trim().ToLower().StartsWith(name)).ToList();
        }

        public List<Book> SelectByAuthor(string authorName)
        {
            return db.Books.Where(x => x.Author.Trim().ToLower().StartsWith(authorName))
                .ToList();
        }

        public List<Book> SelectByAuthorOrName(string str)
        {
            return db.Books.Where(
            x => x.Author.Trim().ToLower().StartsWith(str) || x.Name.Trim().ToLower().StartsWith(str)
                ).ToList();
        }

        public List<Book> SelectAvailableBooks()
        {
            /*var borrowedBookIds = from borrow in db.BorrowedBooks
                                  select borrow.BookId;

            var availableBooks = from book in db.Books
                                     //join borrowed in db.BorrowedBooks
                                 where !borrowedBookIds.Contains(book.Id)
                                 select book;*/
            var availableBooks = db.Books.Where(book => book.BorrowedBooks.All(history => history.ReturnDate <= DateTime.Now));


            return availableBooks.ToList();


        }

        /*public List<BookInfo> SelectBooksCustomColumn()
        {
            List<BookInfo> resultList = db.Books.Select(b => new BookInfo { Title = b.Name, Author = b.Author }).ToList();
            return resultList;
        }*/
    }
}