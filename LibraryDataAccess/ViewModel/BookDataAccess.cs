﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace LibraryDataAccess.ViewModel
{
    
    public class BookDataAccess:IBookDataAccess
    {
       
        LibraryEntities db ;
        
        public BookDataAccess(LibraryEntities libraryEntities)
        {
            db= libraryEntities;
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
            catch {return false; }
        }
        public void Save() {
            db.SaveChanges();
        }

        public List<Book> SelectByName(string name)
        {
            return db.Books.Where(x => x.Name.Trim().ToLower().StartsWith(name)).ToList();
        }

        public List<Book> SelectByAuthor(string authorName)
        {
            return db.Books.Where(x => x.Author.Trim().ToLower().StartsWith(authorName)).ToList();
        }

        public List<Book> SelectByAuthorOrName(string str)
        {
            return db.Books.Where(
            x => x.Author.Trim().ToLower().StartsWith(str) || x.Name.Trim().ToLower().StartsWith(str)
                ).ToList();
        }

        public List<Book> SelectBooksNotBorrowed()
        {
            
        }
    }
}