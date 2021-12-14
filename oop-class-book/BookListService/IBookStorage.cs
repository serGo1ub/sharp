using System.Collections.Generic;
using BookClass;

namespace BookListService
{
    public interface IBookStorage
    {
        public IEnumerable<Book> GetBooks();

        public void AddBooks(IEnumerable<Book> books);
    }
}
