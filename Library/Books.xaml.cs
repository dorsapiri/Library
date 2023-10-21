using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LibraryDataAccess;
using LibraryDataAccess.contex;
using LibraryDataAccess.ViewModel;

namespace Library
{
    /// <summary>
    /// Interaction logic for Books.xaml
    /// </summary>
    public partial class Books : Window
    {
        private BookDataAccess bookDataAccess;
        
        private MemberDataAccess memberDataAccess;
        
        
       
        
        public Book currentBook { get; set; } = new Book();
        public Member currentMember { get; set; } = new Member();
        public Books(Member Member)
        {
            InitializeComponent();
            FillData();
            
            currentMember = Member;
        }
        private void FillData() { 
            using(UnitOfWork db = new UnitOfWork())
            {
                dgBooks.ItemsSource = db.IBookDataAccess.SelectAvailableBooks();
            }
            
            
        }

        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgBooks.SelectedIndex > 0) {
                currentBook = dgBooks.SelectedItem as Book;
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork()) {
                if (currentBook != null)
                {
                    BorrowedBook bookBorrowed = new BorrowedBook()
                    {
                        BookId = currentBook.Id,
                        MemberId = currentMember.Id,
                        BorrowedDate = DateTime.Now,
                        ReturnDate = DateTime.Now.AddDays(7)
                    };

                    db.IBorrowedBookDataAccess.Insert(bookBorrowed);
                    db.IBorrowedBookDataAccess.Save();
                }
            }
            
            this.Close();

        }
    }
}
