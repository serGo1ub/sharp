using System.Collections.Generic;
using BookClass;

namespace BookListService
{
    public class BookStorage: IBookStorage
    {
        private List<Book> books;

        public bool isEmpty()
        {
            return books.Count == 0;
        }
        
        public BookStorage()
        {
            books = new List<Book>();
        }
        
        public IEnumerable<Book> GetBooks()
        {
            return books;
        }

        public void AddBooks(IEnumerable<Book> books)
        {
            foreach (var item in books)
            {
                this.books.Add(item);
            }
        }
    }
}
