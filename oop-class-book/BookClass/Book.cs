using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using VerificationService;

namespace BookClass
{
    /// <summary>
    /// Represents the book as a type of publication.
    /// </summary>
    public sealed class Book : IEquatable<Book>, IComparable<Book>, IComparable
    {
        private bool published;

        private DateTime datePublished;

        private int totalPages;

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        /// <param name="author">Author of the book.</param>
        /// <param name="title">Title of the book.</param>
        /// <param name="publisher">Publisher of the book.</param>
        /// <exception cref="ArgumentNullException">Throw when author or title or publisher is null.</exception>
        public Book(string author, string title, string publisher) 
            : this(author, title, publisher, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        /// <param name="author">Author of the book.</param>
        /// <param name="title">Title of the book.</param>
        /// <param name="publisher">Publisher of the book.</param>
        /// <param name="isbn">International Standard Book Number.</param>
        /// <exception cref="ArgumentNullException">Throw when author or title or publisher or ISBN is null.</exception>
        public Book(string author, string title, string publisher, string isbn)
        {
            this.Author = author is null ? throw new ArgumentNullException("author or title or publisher or ISBN cannot be null") : author;
            this.Title = title is null ? throw new ArgumentNullException("author or title or publisher or ISBN cannot be null") : title;
            this.Publisher = publisher is null ? throw new ArgumentNullException("author or title or publisher or ISBN cannot be null") : publisher;

            if (isbn == string.Empty)
            {
                this.ISBN = isbn;
                return;
            }

            if (!isbn.IsValid())
            {
                throw new ArgumentException("ISBN is not valid.");
            }

            this.ISBN = isbn;
        }

        /// <summary>
        /// Gets author of the book.
        /// </summary>
        public string Author { get; } // readonly field + get accessor

        /// <summary>
        /// Gets title of the book.
        /// </summary>
        public string Title { get; } // readonly field + get accessor

        /// <summary>
        /// Gets publisher of the book.
        /// </summary>
        public string Publisher { get; } // readonly field + get accessor

        /// <summary>
        /// Gets or sets total pages in the book.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throw when Pages less or equal zero.</exception>
        public int Pages
        {
            get => this.totalPages;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Pages cannot be less or equal zero.");
                }

                this.totalPages = value;
            }
        }

        /// <summary>
        /// Gets International Standard Book Number.
        /// </summary>
        public string ISBN { get; } // readonly field + get accessor

        /// <summary>
        /// Gets price.
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// Gets currency.
        /// </summary>

        public string Currency { get; private set; }

        /// <summary>
        /// Publishes the book if it has not yet been published.
        /// </summary>
        /// <param name="dateTime">Date of publish.</param>
        public void Publish(DateTime dateTime)
        {
            this.published = true;
            this.datePublished = dateTime;
        }

        /// <summary>
        /// String representation of book.
        /// </summary>
        /// <returns>Representation of book.</returns>
        public override string ToString() => $"{this.Title} by {this.Author}";

        /// <summary>
        /// Gets a information about time of publish.
        /// </summary>
        /// <returns>The string "NYP" if book not published, and the value of the datePublished if it is published.</returns>
        public string GetPublicationDate()
        {
            return this.published ? this.datePublished.ToString("d", CultureInfo.InvariantCulture) : "NYP";
        }

        /// <summary>
        /// Sets the prise and currency of the book.
        /// </summary>
        /// <param name="price">Price of book.</param>
        /// <param name="currency">Currency of book.</param>
        /// <exception cref="ArgumentException">Throw when Price less than zero or currency is invalid.</exception>
        /// <exception cref="ArgumentNullException">Throw when currency is null.</exception>

        public void SetPrice(decimal price, string currency)
        {
            if (price < 0)
            {
                throw new ArgumentException("Price cannot be less than zero.", nameof(price));
            }

            if (currency is null)
            {
                throw new ArgumentNullException(nameof(currency));
            }

            if (!IsoCurrencyValidator.IsValid(currency))
            {
                throw new ArgumentException("Invalid currency string.", nameof(currency));
            }

            this.Price = price;
            this.Currency = currency;
        }

        /// <summary>
        /// Compare books based on their ISBN.
        /// </summary>
        public override bool Equals([AllowNull] Object obj)
        {
            Book book = obj as Book;

            return book == null ? false : base.Equals((Book)obj) && this.ISBN == book.ISBN;
        }
        
        /// <summary>
        /// Compare books based on their ISBN.
        /// </summary>
        public bool Equals([AllowNull] Book otherBook)
        {
            if (otherBook == null) return false;

            return Author.Equals(otherBook.Author) && Title.Equals(otherBook.Title) && Publisher.Equals(otherBook.Publisher) && ISBN == otherBook.ISBN;
        }
        
        /// <summary>
        /// Return the hashcode of this Book object.
        /// </summary>
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(this.ISBN);
            return hash.ToHashCode();
        }

        /// <summary>
        /// Returns the title comparison result.
        /// </summary>
        public int CompareTo([AllowNull] Book otherBook)
        {
            if (otherBook == null)
            {
                return -1;
            }
    
            return String.Compare(this.Title, otherBook.Title);
        }

        /// <summary>
        /// Returns the title comparison result.
        /// </summary>
        public int CompareTo([AllowNull] object obj)
        {
            if (obj is null)
            {
                return -1;
            }
            
            Book book = obj as Book;
            
            return String.Compare(this.Title, book.Title);
        }
    }
}
