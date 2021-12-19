using System;
using System.Collections.Generic;
using System.Linq;
using BookClass;

namespace BookListService
{
    public class BookListService
    {
        private Dictionary<int, Book> BookList = new Dictionary<int, Book>();

        public bool isEmpty()
        {
            return BookList.Count == 0;
        }

        public void Add(Book book)
        {
            if (BookList.ContainsValue(book))
            {
                throw new Exception("This book is already exist");
            } 
            BookList.Add(BookList.Count, book);
        }

        public void Remove(Book book)
        {
            if (BookList.Count == 0)
            {
                return;
            } 
            
            if (!BookList.ContainsValue(book))
            {
                throw new Exception("Failed to remove the book");
            }
            
                
            foreach (var bookItem in BookList)
            {
                if (bookItem.Value == book)
                {
                    BookList.Remove(bookItem.Key);
                    return;
                }
            }

        }
        
        public List<Book> FindBy(Predicate<Book> predicate)
        {
            return BookList.Values.ToList().FindAll(predicate);
        }

        public List<Book> GetBy(IComparer<Book> comparer)
        {
            List<Book> books = BookList.Values.ToList();
            books.Sort(comparer);
            return books;
        }

        public void Load(IBookStorage bookStorage)
        {
            var books = bookStorage.GetBooks();
            int count = 0;

            foreach (var item in books)
            {
                BookList.Add(count++, item);
            }
        }
        
        public void Save(IBookStorage bookStorage) 
        {
            List<Book> books = new List<Book>();

            foreach (var bookItem in BookList)
            {
                books.Add(bookItem.Value);
            }

            bookStorage.AddBooks(books);
        }
    }
}
