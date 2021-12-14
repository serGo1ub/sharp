using System;
using System.Collections.Generic;
using BookClass;

namespace BookListService
{
    public class BookListService
    {
        private Dictionary<int, Book> BookList = new Dictionary<int, Book>();

        public Dictionary<int, Book> GetBookList()
        {
            return BookList;
        }

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
        public List<Book> FindByAuthor(string Author)
        {
            List<Book> books = new List<Book>();

            foreach (var bookItem in BookList)
            {
                if (bookItem.Value.Author.Equals(Author))
                {
                    books.Add(bookItem.Value);
                }
            }

            return books;
        }

        public List<Book> FindByTitle(string Title)
        {
            List<Book> books = new List<Book>();

            foreach (var bookItem in BookList)
            {
                if (bookItem.Value.Title.Equals(Title))
                {
                    books.Add(bookItem.Value);
                }
            }

            return books;
        }

        public List<Book> FindByPublisher(string Publisher)
        {
            List<Book> books = new List<Book>();

            foreach (var bookItem in BookList)
            {
                if (bookItem.Value.Publisher.Equals(Publisher))
                {
                    books.Add(bookItem.Value);
                }
            }

            return books;
        }
        public List<Book> GetBy(string fieldName)
        {
            List<Book> books = new List<Book>();

            foreach (var bookItem in BookList)
            {
                books.Add(bookItem.Value);   
            }

            if (fieldName == "Author")
            {
                books.Sort(new AuthorComparator());                
            } else if (fieldName == "Pages")
            {
                books.Sort(new PagesComparator());
            } else if (fieldName == "Price")
            {
                books.Sort(new PriceComparator());
            }
            else
            {
                return books;
            }

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
