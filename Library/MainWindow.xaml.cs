using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryDataAccess;
using LibraryDataAccess.ViewModel;
using LibraryDataAccess.contex;




namespace Library
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //instance of accesses
        MemberDataAccess memberDataAccess;
        BookDataAccess bookDataAccess;
        

        ObservableCollection<Member> members = new ObservableCollection<Member>();
        ObservableCollection<Book> books = new ObservableCollection<Book>();
        
        



        public Member currentMember { get; set; } = new Member();
        public Book currentBook { get; set; } = new Book();
        

        public MainWindow()
        {
            InitializeComponent();
            fillData();
            
        }
        public void fillData() {
            
            using (UnitOfWork db = new UnitOfWork())
            {
                dgBooks.ItemsSource = db.IBookDataAccess.SelectAll();
                dgBooks.AutoGenerateColumns = false;
                dgMembers.ItemsSource = db.IMemberDataAccess.SelectAll();
                dgMembers.AutoGenerateColumns = false;
            }
        }
       

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditBook addBook = new AddOrEditBook(bookDataAccess);
            addBook.Show();
        }

        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAddMember_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditMember addMember = new AddOrEditMember(memberDataAccess);
            addMember.Show();
        }

        private void dgBooks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            currentBook = dgBooks.SelectedItem as Book;
            AddOrEditBook editBook = new AddOrEditBook(bookDataAccess , currentBook);
            editBook.Show();
        }

        private void dgMembers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            currentMember = dgMembers.SelectedItem as Member;
            AddOrEditMember editMember = new AddOrEditMember(memberDataAccess ,currentMember);
            editMember.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            fillData();
        }

        private void tbBookSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchItem = tbBookSearch.Text.Trim().ToLower();
            using(UnitOfWork db = new UnitOfWork())
            {
                dgBooks.ItemsSource = db.IBookDataAccess.SelectByAuthorOrName(searchItem);
            }
            
        }

        private void tbMemberSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchItem = tbMemberSearch.Text.Trim().ToLower();
            using (UnitOfWork db = new UnitOfWork())
            {
                dgMembers.ItemsSource = db.IMemberDataAccess.SelectByName(searchItem);
            }
        }
    }
}
