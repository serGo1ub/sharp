using System;
using NUnit.Framework;

#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable SA1600 // Elements should be documented

namespace VerificationService.Tests
{
    [TestFixture]
    public class VerificationTests
    {
        [TestCase("aboba")]
        [TestCase("!0V")]
        [TestCase("1234")]
        public void isoCurrencyValidatorIsNotValie(string currency)
        {
            Assert.IsFalse(IsoCurrencyValidator.IsValid(currency));
        }

        [TestCase("")]
        [TestCase(null)]
        public void isoCurrencyValidatorThrowException(string currency)
        {
            Assert.Throws<ArgumentException>(() => IsoCurrencyValidator.IsValid(currency), "currency is not valid");
        }
        
        [TestCase("1-221-63415-1")]
        [TestCase("3-384-14621-8")]
        public void isbnVerifier10(string isbn)
        {
            Assert.IsTrue(IsbnVerifier.IsValid(isbn));
        }
        
        [TestCase("836-2-333-54457-9")]
        [TestCase("473-4-888-11236-8")]
        public void isbnVerifier13(string isbn)
        {
            Assert.IsTrue(IsbnVerifier.IsValid(isbn));
        }
        
        [TestCase("05332112-4121232-676-8912319043493843927")]
        [TestCase("581-1-12113-757-5")]
        [TestCase("413-52-156-8")]
        [TestCase("3-1-448-1132132")]
        public void isbnVerifierIsNotValid(string isbn)
        {
            Assert.IsFalse(IsbnVerifier.IsValid(isbn));
        }

        
        [TestCase("EUR")]
        [TestCase("USD")]
        public void isoCurrencyValidatorTests(string currency)
        {
            Assert.IsTrue(IsoCurrencyValidator.IsValid(currency));
        }
        
    }
}
