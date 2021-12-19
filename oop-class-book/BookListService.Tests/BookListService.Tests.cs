using System;
using System.Collections.Generic;
using BookClass;
using NUnit.Framework;

namespace BookListService.Tests
{
    [TestFixture]
    public class BookServiceTests
    {
        [TestCase("Kyle Simpson", "You do not know JS", "Peter")]
        public void BookService_Add_Book(string author, string title, string publisher)
        {
            BookListService bookListService = new BookListService();
            Book book = new Book(author, title, publisher);

            bookListService.Add(book);
            Assert.IsFalse(bookListService.isEmpty());
        }

        [TestCase("Kyle Simpson", "You do not know JS", "Peter")]
        public void BookService_Add_Same_Book(string author, string title, string publisher)
        {
            BookListService bookListService = new BookListService();
            Book book = new Book(author, title, publisher);

            bookListService.Add(book);
            Assert.Throws<Exception>(() => bookListService.Add(book), "This book is already exist");
        }

        [TestCase("Kyle Simpson", "You do not know JS", "Peter")]
        public void BookService_Remove_Book(string author, string title, string publisher)
        {
            BookListService bookListService = new BookListService();
            Book book = new Book(author, title, publisher);

            bookListService.Add(book);
            bookListService.Remove(book);
            Assert.IsTrue(bookListService.isEmpty());
        }

        [TestCase("Kyle Simpson", "You do not know JS", "Peter")]
        public void BookService_Remove_Unexisting_Book(string author, string title, string publisher)
        {
            BookListService bookListService = new BookListService();
            Book book = new Book(author, title, publisher);
            bookListService.Add(new Book(string.Empty, string.Empty, string.Empty));

            Assert.Throws<Exception>(() => bookListService.Remove(book), "Failed to remove the book");
        }

        [TestCase("Kyle Simpson")]
        public void BookService_FindByAuthor(string author)
        {
            BookListService bookListService = new BookListService();

            Book book1 = new Book(string.Empty, string.Empty, "Peter");
            Book book2 = new Book(author, string.Empty, string.Empty);
            Book book3 = new Book(string.Empty, "You do not know JS", string.Empty);
            Book book4 = new Book(string.Empty, string.Empty, string.Empty);
            Book book5 = new Book(author, "You do not know JS", string.Empty);

            bookListService.Add(book1);
            bookListService.Add(book2);
            bookListService.Add(book3);
            bookListService.Add(book4);
            bookListService.Add(book5);
            
            List<Book> list = bookListService.FindBy(book => book.Author.Equals(author));

            Assert.IsTrue(list.Contains(book2) && list.Contains(book5) && list.Count == 2);
        }

        [TestCase("You do not know JS")]
        public void BookService_FindByTitle(string title)
        {
            BookListService bookListService = new BookListService();

            Book book1 = new Book(string.Empty, string.Empty, "Peter");
            Book book2 = new Book("Kyle Simpson", string.Empty, string.Empty);
            Book book3 = new Book(string.Empty, title, string.Empty);
            Book book4 = new Book(string.Empty, string.Empty, string.Empty);
            Book book5 = new Book("Kyle Simpson", title, string.Empty);

            
            bookListService.Add(book1);
            bookListService.Add(book2);
            bookListService.Add(book3);
            bookListService.Add(book4);
            bookListService.Add(book5);
            
            List<Book> list = bookListService.FindBy(book => book.Title.Equals(title));

            Assert.IsTrue(list.Contains(book3) && list.Contains(book5) && list.Count == 2);
        }

        [TestCase("Peter")]
        public void BookService_FindByPublisher(string publisher)
        {
            BookListService bookListService = new BookListService();

            Book book1 = new Book(string.Empty, string.Empty, publisher);
            Book book2 = new Book("Kyle Simpson", string.Empty, string.Empty);
            Book book3 = new Book(string.Empty, "You do not know JS", string.Empty);
            Book book4 = new Book(string.Empty, string.Empty, string.Empty);
            Book book5 = new Book("Kyle Simpson", "You do not know JS", string.Empty);

            bookListService.Add(book1);
            bookListService.Add(book2);
            bookListService.Add(book3);
            bookListService.Add(book4);
            bookListService.Add(book5);
            
            List<Book> list = bookListService.FindBy(book => book.Publisher.Equals(publisher));

            Assert.IsTrue(list.Contains(book1) && list.Count == 1);
        }

        [TestCase("C", "A", "B")]
        public void BookService_GetByAuthor(string author1, string author2, string author3)
        {
            BookListService bookListService = new BookListService();

            Book book1 = new Book(author1, string.Empty, string.Empty);
            Book book2 = new Book(author2, string.Empty, string.Empty);
            Book book3 = new Book(author3, string.Empty, string.Empty);
   
            bookListService.Add(book1);
            bookListService.Add(book2);
            bookListService.Add(book3);
            
            List<Book> list = bookListService.GetBy(new AuthorComparator());

            Assert.IsTrue(list[0] == book2 && list[1] == book3 && list[2] == book1);
        }

        [TestCase(300, 40, 230)]
        public void BookService_GetByPages(int pages1, int pages2, int pages3)
        {
            BookListService bookListService = new BookListService();

            Book book1 = new Book("A", string.Empty, string.Empty) { Pages = pages1 };
            Book book2 = new Book("B", string.Empty, string.Empty) { Pages = pages2 };
            Book book3 = new Book("C", string.Empty, string.Empty) { Pages = pages3 };

            bookListService.Add(book1);
            bookListService.Add(book2);
            bookListService.Add(book3);
            
            List<Book> list = bookListService.GetBy(new PagesComparator());

            Assert.IsTrue(list[0] == book2 && list[1] == book3 && list[2] == book1);
        }

        [TestCase(300, 40, 230, "USD")]
        public void BookService_GetByPrice(int price1, int price2, int price3, string currency)
        {
            BookListService bookListService = new BookListService();

            Book book1 = new Book("A", string.Empty, string.Empty);
            Book book2 = new Book("B", string.Empty, string.Empty);
            Book book3 = new Book("C", string.Empty, string.Empty);

            book1.SetPrice(price1, currency);
            book2.SetPrice(price2, currency);
            book3.SetPrice(price3, currency);

            bookListService.Add(book1);
            bookListService.Add(book2);
            bookListService.Add(book3);

            List<Book> list = bookListService.GetBy(new PriceComparator());

            Assert.IsTrue(list[0] == book2 && list[1] == book3 && list[2] == book1);
        }

        [Test]
        public void BookService_Save_Into_BookStorage()
        {
            BookListService bookListService = new BookListService();

            Book book1 = new Book("A", string.Empty, string.Empty);
            Book book2 = new Book("B", string.Empty, string.Empty);
            Book book3 = new Book("C", string.Empty, string.Empty);

            bookListService.Add(book1);
            bookListService.Add(book2);
            bookListService.Add(book3);

            BookStorage bookStorage = new BookStorage();

            bookListService.Save(bookStorage);
            
            Assert.IsFalse(bookStorage.isEmpty());
        }
    }
}
