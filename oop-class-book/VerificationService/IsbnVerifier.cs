using System;
using System.Text.RegularExpressions;

namespace VerificationService
{
    /// <summary>
    /// Verifies if the string representation of number is a valid ISBN-10 or ISBN-13 identification number of book.
    /// </summary>
    public static class IsbnVerifier
    {
        /// <summary>
        /// Verifies if the string representation of number is a valid ISBN-10 or ISBN-13 identification number of book.
        /// </summary>
        /// <param name="isbn">The string representation of book's isbn.</param>
        /// <returns>true if number is a valid ISBN-10 or ISBN-13 identification number of book, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if isbn is null.</exception>
        public static bool IsValid(this string isbn)
        {
            Regex ISBNTen = new Regex(@"\d-\d{3}-\d{5}-\d");
            Regex ISBNThirteen = new Regex(@"\d{3}-\d-\d{3}-\d{5}-\d");

            switch (isbn.Length)
            {
                case 0: return true;
                case 13: return ISBNTen.IsMatch(isbn);
                case 17: return ISBNThirteen.IsMatch(isbn);
            }

            return false;
        }
    }
}
