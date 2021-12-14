using System;
using Microsoft.VisualBasic.CompilerServices;
using NUnit.Framework;

#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable SA1600 // Elements should be documented

namespace BookClass.Tests
{
    [TestFixture]
    public class BookTests
    {
        [Test]
        public void Book_SetPrice_InvalidPrice_ArgumentException()
        {
            decimal price = -5;
            string currency = "USD";
            Book book = new Book(string.Empty, string.Empty, string.Empty);

            Assert.Throws<ArgumentException>(() => book.SetPrice(price, currency), "Price cannot be less than zero.");
        }

        [Test]
        public void Book_SetPrice_BothInvalidArguments_ArgumentException()
        {
            decimal price = -4.44m;
            string currency = null;
            Book book = new Book(string.Empty, string.Empty, string.Empty);

            Assert.Throws<ArgumentException>(() => book.SetPrice(price, currency), "Price cannot be less than zero.");
        }

        [TestCase("", "", "", ExpectedResult = " by ")]
        public string Book_ToString_WithEmptyFields(string author, string title, string publisher) 
            => new Book(author, title, publisher).ToString();

        [TestCase("Jon Skeet", "C# in Depth", "Manning Publications")]
        public void Book_CreateNewThreeParameters(string author, string title, string publisher)
        {
            Book book = new Book(author, title, publisher); 
            Assert.IsTrue(book.Author == author && book.Title == title && book.Publisher == publisher); 
        }

        [TestCase("Jon Skeet", "C# in Depth", "Manning Publications", "978-0-901-69066-1")]
        [TestCase("Jon Skeet", "C# in Depth", "Manning Publications", "3-598-21508-8")]
        [TestCase("Jon Skeet", "C# in Depth", "Manning Publications", "")]
        public void Book_CreateNewFourParameters(string author, string title, string publisher, string isbn)
        {
            Book book = new Book(author, title, publisher, isbn);
            Assert.IsTrue(book.Author == author && book.Title == title && book.Publisher == publisher && book.ISBN == isbn);
        }

        [TestCase(null, "C# in Depth", "Manning Publications")]
        [TestCase("Jon Skeet", null, "Manning Publications")]
        [TestCase("Jon Skeet", "C# in Depth", null)]
        public void Book_CreateNewThreeParameters_ThrowArgumentNullException(string author, string title, string publisher)
        {
            Assert.Throws<ArgumentNullException>(() => new Book(author, title, publisher), "author or title or publisher cannot be null");
        }

        [TestCase(null, "C# in Depth", "Manning Publications", "978-0-901-69066-1")]
        [TestCase("Jon Skeet", null, "Manning Publications", "978-0-901-69066-1")]
        [TestCase("Jon Skeet", "C# in Depth", null, "978-0-901-69066-1")]
        public void Book_CreateNewFourParameters_ThrowArgumentNullException(string author, string title, string publisher, string isbn)
        {
            Assert.Throws<ArgumentNullException>(() => new Book(author, title, publisher, isbn), "author or title or publisher or ISBN cannot be null");
        }

        [Test]
        public void Book_PagesTest()
        {
            int expected = 10;
            Book book = new Book(string.Empty, string.Empty, string.Empty)
            {
                Pages = expected,
            };
            Assert.AreEqual(expected, book.Pages);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Book_PagesTest_ArgumentOutOfRangeException(int pages)
        {
            Book book = new Book(string.Empty, string.Empty, string.Empty);
            Assert.Throws<ArgumentOutOfRangeException>(() => book.Pages = pages, "Count of pages should be greater than zero.");
        }

        [Test]
        public void Book_Publish_GetPublicationDate_Tests()
        {
            DateTime expected = DateTime.Now;
            Book book = new Book(string.Empty, string.Empty, string.Empty);
            book.Publish(expected);

            Assert.AreEqual(FormattableString.Invariant($"{expected:d}"), book.GetPublicationDate());
        }

        [Test]
        public void Book_Publish_GetPublicationDate_Empty_Tests()
        {
            string expected = "NYP";
            Book book = new Book(string.Empty, string.Empty, string.Empty);
            Assert.AreEqual(expected, book.GetPublicationDate());
        }

        [Test]
        public void Book_SetPrice_Tests()
        {
            decimal price = 4.44m;
            string currency = "USD";
            Book book = new Book(string.Empty, string.Empty, string.Empty);
            book.SetPrice(price, currency);
            Assert.IsTrue(book.Price == price && book.Currency == currency);
        }

        [Test]
        public void Book_SetPrice_InvalidCurrency_ArgumentException()
        {
            decimal price = 4.44m;
            string currency = "_~_";
            Book book = new Book(string.Empty, string.Empty, string.Empty);

            Assert.Throws<ArgumentException>(() => book.SetPrice(price, currency), "Currency is invalid.");
        }

        [Test]
        public void Book_SetPrice_LessZero_ArgumentException()
        {
            decimal price = -4.44m;
            string currency = "USD";
            Book book = new Book(string.Empty, string.Empty, string.Empty);

            Assert.Throws<ArgumentException>(() => book.SetPrice(price, currency), "Price cannot be less than zero.");
        }

        [Test]
        public void Book_SetPrice_CurrencyIsNull_ArgumentNullException()
        {
            decimal price = 4.44m;
            Book book = new Book(string.Empty, string.Empty, string.Empty);

            Assert.Throws<ArgumentNullException>(() => book.SetPrice(price, null), "Currency cannot be null.");
        }

        [TestCase("Jon Skeet", "C# in Depth", "Manning Publications", ExpectedResult = "C# in Depth by Jon Skeet")]
        [TestCase("Jon Skeet", "", "Manning Publications", ExpectedResult = " by Jon Skeet")]
        [TestCase("", "C# in Depth", "Manning Publications", ExpectedResult = "C# in Depth by ")]
        public string Book_ToString(string author, string title, string publisher)
            => new Book(author, title, publisher).ToString();

        [TestCase("512-4-676-89127-0", "512-4-676-89127-0", ExpectedResult = true)]
        [TestCase("512-4-676-89127-0", "8-482-12697-3", ExpectedResult = false)]
        [TestCase("512-4-676-89127-0", "", ExpectedResult = false)]
        [TestCase("", "512-4-676-89127-0", ExpectedResult = false)]
        public bool Book_Equals(string ISBN1, string ISBN2)
        {
            return new Book(string.Empty, string.Empty, string.Empty, ISBN1)
                .Equals(new Book(string.Empty, string.Empty, string.Empty, ISBN2));
        }
        
        [TestCase("Jon Skeet", "C# in Depth", "Manning Publications", true)]
        [TestCase("Jon Skeet", "C# in Depth", "Manning Publications", false)]
        [TestCase("Jon Skeet", "", "Manning Publications", true)]
        [TestCase("", "C# in Depth", "Manning Publications", false)]
        public void Book_GetHashCode(string author, string title, string publisher, bool published)
        {
            Book book = new Book(author, title, publisher);
            if (published)
            {
                book.Publish(DateTime.Now);
            }

            Assert.IsTrue(book.GetHashCode() is int);
        }

        [TestCase("C# in Depth", "C# in Depth", ExpectedResult = 0)]
        [TestCase("C# in Depth", "Some book", ExpectedResult = -1)]
        [TestCase("C# in Depth", "C in Depthhhhh", ExpectedResult = 1)]
        public int Book_CompareTo_Title(string Title1, string Title2)
        {
            return new Book(string.Empty, Title1, string.Empty)
                .CompareTo(new Book(string.Empty, Title2, string.Empty));
        }
        
        [TestCase("Jon Skeet", "Jon Skeet", ExpectedResult = 0)]
        [TestCase("ABOBA", "BIDON", ExpectedResult = -1)]
        [TestCase("BIDON", "ABOBA", ExpectedResult = 1)]
        public int Book_Author_Comparison_Test(string Author1, string Author2)
        {
            return new AuthorComparator().Compare(new Book(Author1, string.Empty, string.Empty), new Book(Author2, string.Empty, string.Empty));
        }

        [TestCase(10, 10, ExpectedResult = 0)]
        [TestCase(11, 10, ExpectedResult = 1)]
        [TestCase(10, 11, ExpectedResult = -1)]
        public int Book_Pages_Comparison_Test(int Pages1, int Pages2)
        {
            return new PagesComparator().Compare(new Book(string.Empty, string.Empty, string.Empty) { Pages = Pages1 }, new Book(string.Empty, string.Empty, string.Empty) { Pages = Pages2 });
        }

        [TestCase(300, "USD", 300, "USD", ExpectedResult = 0)]
        [TestCase(300, "USD", 30, "USD", ExpectedResult = 1)]
        [TestCase(40, "UAH", 300, "USD", ExpectedResult = -1)]
        [TestCase(10, "UAH", 10, "USD", ExpectedResult = 0)]
        public int Book_Price_Comparison_Test(int Price1, string Currency1, int Price2, string Currency2)
        {
            Book book1 = new Book(string.Empty, string.Empty, string.Empty);
            book1.SetPrice(Price1, Currency1);

            Book book2 = new Book(string.Empty, string.Empty, string.Empty);
            book2.SetPrice(Price2, Currency2);

            return new PriceComparator().Compare(book1, book2);
        }
    }
}