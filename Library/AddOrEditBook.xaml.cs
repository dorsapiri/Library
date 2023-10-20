using LibraryDataAccess;
using LibraryDataAccess.contex;
using LibraryDataAccess.ViewModel;
using System;
using System.Collections.Generic;
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

namespace Library
{
    /// <summary>
    /// Interaction logic for AddOrEditBook.xaml
    /// </summary>
    public partial class AddOrEditBook : Window
    {
        private BookDataAccess BookDataAccess;
        private Book editingBook;
        private bool isEdit = false;
        public AddOrEditBook(BookDataAccess bookDataAccess)
        {
            InitializeComponent();
            BookDataAccess = bookDataAccess;
            this.Title = "Add a book";
            btnDeleteBook.IsEnabled = false;
        }
        public AddOrEditBook(BookDataAccess bookDataAccess , Book book)
        {
            InitializeComponent();
            BookDataAccess = bookDataAccess;
            editingBook = book;
            isEdit = true;

            this.Title = "Edit book";
            tbBookname.Text = editingBook.Name;
            tbAuthor.Text = editingBook.Author;
         
        }
        public void ImageSet() { 
            
        }

        private void btnSaveBook_Click(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                if (isBookNameValid())
                {
                    if (isEdit)
                    {
                        Book book = new Book()
                        {
                            Name = tbBookname.Text,
                            Author = tbAuthor.Text,
                            Id = editingBook.Id,
                            Picture = "/Images/.."
                        };
                        db.IBookDataAccess.Update(book);
                        db.IBookDataAccess.Save();
                        this.Close();
                    }
                    else
                    {
                        Book book = new Book()
                        {
                            Name = tbBookname.Text,
                            Author = tbAuthor.Text,
                            Picture = "/Images/.."
                        };
                        if (isNotExist(book)) {
                            db.IBookDataAccess.Insert(book);
                            db.IBookDataAccess.Save();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("This book is already Exist!","Warning",MessageBoxButton.OKCancel, MessageBoxImage.Error);
                        }
                        
                    }
                    
                }
            }  
        }
        private bool isNotExist(Book bookname) {
            bool isExist = false;
            using (UnitOfWork db = new UnitOfWork())
            {
                if (!db.IBookDataAccess.SelectByName(bookname.Name).Any()) {
                    return true;
                }
            }
            return isExist;
        }
        private bool isBookNameValid() {
            bool isValid = true;
            String Name = tbBookname.Text.Trim().ToLower();
            String Author = tbAuthor.Text.Trim().ToLower();

            if (String.IsNullOrEmpty(Name))
            {
                isValid = false;
                MessageBox.Show("Book Name is not valid!", "Warning!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            } else if (String.IsNullOrEmpty(Author))
            {
                isValid = false;
                MessageBox.Show("Author is not valid!", "Warning!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            return isValid;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            using(UnitOfWork db = new UnitOfWork())
            {
                if (isEdit)
                {
                    db.IBookDataAccess.Delete(editingBook);
                    db.IBookDataAccess.Save();
                    
                }
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.fillData();
            this.Close();
            
           
        }
    }
}
