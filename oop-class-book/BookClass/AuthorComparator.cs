using System.Collections.Generic;

namespace BookClass
{
    /// <summary>
    /// authorComparator
    /// </summary>
    public class AuthorComparator : IComparer<Book>
    {
        /// <summary>
        /// Returns the author comparison result.
        /// </summary>
        public int Compare(Book book1, Book book2)
        {
            if (book1 is null)
            {
                return -1;
            }

            if (book2 is null)
            {
                return 1;
            }
            
            return book1.Author.CompareTo(book2.Author);
        }
    }
}
