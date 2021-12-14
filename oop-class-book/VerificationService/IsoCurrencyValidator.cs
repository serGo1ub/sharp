using System;
using System.Globalization;

namespace VerificationService
{
    /// <summary>
    /// Class for validating currency strings.
    /// </summary>
    public static class IsoCurrencyValidator
    {
        /// <summary>
        /// Determines whether a specified string is a valid ISO currency symbol.
        /// </summary>
        /// <param name="currency">Currency string to check.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="currency"/> is a valid ISO currency symbol; <see langword="false"/> otherwise.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if currency is null or empty or whitespace or white-space.</exception>
        public static bool IsValid(string currency)
        {
            if (currency is null || currency == "")
            {
                throw new ArgumentException("currency is not valid");
            }

            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            RegionInfo region;
            for (int i = 0; i < cultures.Length; i++)
            {
                try
                {
                    region = new RegionInfo(cultures[i].LCID);
                }
                catch (ArgumentException)
                {
                    continue;
                }
                if (region.ISOCurrencySymbol == currency)
                {
                    return true;
                }
            }
    
            return false;
        }
    }
}
