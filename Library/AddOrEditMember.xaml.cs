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
    /// Interaction logic for AddOrEditMember.xaml
    /// </summary>
    public partial class AddOrEditMember : Window
    {
        private MemberDataAccess memberDataAccess;
        private Member editingMember;
        private Member currentNewMember;
        private Book SelectedBook;

        private bool isEditing = false;
        public AddOrEditMember(MemberDataAccess mda)
        {
            InitializeComponent();
            memberDataAccess = mda;
            this.Title = "Add a Member";
            btnDeleteBorrowedBook.IsEnabled = false;
            btnDeleteMember.IsEnabled = false;
        }
        public AddOrEditMember(MemberDataAccess mda , Member member)
        {
            InitializeComponent();
            memberDataAccess = mda;
            editingMember = member;

            this.Title = "Edit Member";
            isEditing = true;

            tbMemberName.Text = editingMember.Name;
            tbDateRegistary.DisplayDate = editingMember.RegistaryDate;
            if(dgBorrowedBook.ItemsSource == null)
            {
                btnDeleteBorrowedBook.IsEnabled = false;
            }
            fillData();
        }
        public void fillData() {
            using(UnitOfWork db = new UnitOfWork())
            {
                dgBorrowedBook.ItemsSource = db.IBorrowedBookDataAccess.GetByMemberId(editingMember.Id);
            }
        }

        private void btnChooseBook_Click(object sender, RoutedEventArgs e)
        {
            if (isEditing)
            {
                Books bookWindow = new Books(editingMember);
                bookWindow.Show();
            }
            else
            {
                using (UnitOfWork db = new UnitOfWork()) {
                    if (tbDateRegistary.ToString() != "") {
                        Member member = new Member()
                        {
                            Name = tbMemberName.Text,
                            RegistaryDate = DateTime.Parse(tbDateRegistary.ToString())
                        };
                        if (isMemberValid() && !isNotExist(member))
                        {
                            db.IMemberDataAccess.Insert(member);
                            db.IMemberDataAccess.Save();
                            currentNewMember = member;
                            Books bookWindow = new Books(currentNewMember);
                            bookWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("This member is Exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please insert a Date for member.", "warning", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                    
                }
                
            }
        }

        private bool isMemberValid() {
            bool isValid = true;
            String Name = tbMemberName.Text.Trim().ToLower();
            //DateTime RegistaryDate = DateTime.Parse(tbDateRegistary.Text);
            if (string.IsNullOrEmpty(Name))
            {
                isValid = false;
                MessageBox.Show("Please insert a name for member.","warning",MessageBoxButton.OK, MessageBoxImage.Error);
            }else if(tbDateRegistary.ToString() == "")
            {
                isValid = false;
                MessageBox.Show("Please insert a Date for member.", "warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return isValid;
        }
        private bool isNotExist(Member member) {
            bool isExist = false;
            using (UnitOfWork db = new UnitOfWork()) {
                if (db.IMemberDataAccess.SelectByName(member.Name).Any()) {
                    isExist =true ;
                }
                
            }
            return isExist;
        }

        private void btnSaveMember_Click(object sender, RoutedEventArgs e)
        {
            using(UnitOfWork db = new UnitOfWork())
            {
                if (isMemberValid()) {
                    if (isEditing)
                    {
                        Member member = new Member()
                        {
                            Id = editingMember.Id,
                            Name = tbMemberName.Text,
                            RegistaryDate = tbDateRegistary.SelectedDate.Value.Date
                        };
                        db.IMemberDataAccess.Update(member);
                        db.IMemberDataAccess.Save();
                        this.Close();
                    }
                    else
                    {
                        if (tbDateRegistary.ToString() != "") {
                            Member member = new Member()
                            {
                                Name = tbMemberName.Text,
                                RegistaryDate = DateTime.Parse(tbDateRegistary.Text)
                            };
                            if (!isNotExist(member))
                            {
                                db.IMemberDataAccess.Insert(member);
                                db.IMemberDataAccess.Save();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("This member is Exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        
                    }
                    
                }
                
            }
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork()) {
                if (!isEditing)
                {
                    db.IMemberDataAccess.Delete(currentNewMember);
                }
            }
            
            this.Close();
        }

        private void btnDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                if (isEditing)
                {
                    db.IMemberDataAccess.Delete(editingMember);
                    
                }
            }
            this.Close();

        }

        private void btnDeleteBorrowedBook_Click(object sender, RoutedEventArgs e)
        {
            using(UnitOfWork db = new UnitOfWork())
            {
                if (isEditing) {
                    BorrowedBook borrowedBook = dgBorrowedBook.SelectedItem as BorrowedBook;
                    db.IBorrowedBookDataAccess.Delete(borrowedBook);
                }
                db.IBorrowedBookDataAccess.Save();
            }
        }
    }
}
