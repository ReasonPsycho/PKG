using System;
using NUnit.Framework;
using PKG;

namespace UnitTests
{
    [TestFixture]
    public class ElGamalGeneratorTests
    {
        [Test]
        public void FindGenerator_Returns_Generator_For_Prime_Number()
        {
            // Arrange
            var p = 23; // prime number
            var expected = 5; // expected generator

            // Act
            var actual = ElGamalGenerator.FindGenerator(p);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindGenerator_Throws_Exception_For_Non_Prime_Number()
        {
            // Arrange
            var p = 24; // non-prime number

            // Act & Assert
            Assert.Throws<Exception>(() => ElGamalGenerator.FindGenerator(p));
        }

        [Test]
        public void Factor_Returns_Prime_Factors_Of_Number()
        {
            // Arrange
            var n = 84; // number to factorize
            int[] expected = { 2, 2, 3, 7 }; // expected prime factors

            // Act
            var actual = ElGamalGenerator.Factor(n);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}