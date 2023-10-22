using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataAccess.ViewModel
{
    public interface IBookDataAccess
    {
        bool Insert(Book newBook);
        bool Delete(Book book);
        List<Book> SelectAll();
        //List<BookInfo> SelectBooksCustomColumn();
        List<Book> SelectByName(string name);
        List<Book> SelectAvailableBooks();
        List<Book> SelectByAuthor(string authorName);
        List<Book> SelectByAuthorOrName(string str);
        bool Update(Book book);
        void Save();
    }
}
