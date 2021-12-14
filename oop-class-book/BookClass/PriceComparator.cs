using System.Collections.Generic;

namespace BookClass
{
    /// <summary>
    /// BookComparator
    /// </summary>
    public class PriceComparator : IComparer<Book>
    {
        /// <summary>
        /// Returns the price comparison result.
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
            
            return book1.Price.CompareTo(book2.Price);
        }
    }
}
