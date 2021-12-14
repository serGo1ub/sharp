using System.Collections.Generic;

namespace BookClass
{
    /// <summary>
    /// bookComparator
    /// </summary>
    public class PagesComparator : IComparer<Book>
    {
        /// <summary>
        /// Returns the pages comparison result.
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
            
            return book1.Pages.CompareTo(book2.Pages);
        }
    }
}
