using System.Collections;
using NUnit.Framework;
using PKG;

namespace UnitTests
{
    [TestFixture]
    public class BitArrayExtensionsTests
    {
        [Test]
        public void LeftShift_ShiftByZero_IsOriginal()
        {
            var bits = new BitArray(new[] { true, false, true, true });
            var expected = new BitArray(bits);
            Assert.AreEqual(expected, bits.LeftShift(0));
        }

        [Test]
        public void LeftShift_ShiftByOne_IsCorrect()
        {
            var bits = new BitArray(new[] { true, false, true, true });
            var expected = new BitArray(new[] { false, true, true, true });
            Assert.AreEqual(expected, bits.LeftShift(1));
        }

        [Test]
        public void ReverseBitArray_LengthIsEven_IsCorrect()
        {
            var bits = new BitArray(new[] { true, false, true, false });
            var expected = new BitArray(new[] { false, true, false, true });
            Assert.AreEqual(expected, bits.ReverseBitArray());
        }

        [Test]
        public void ReverseBitArray_LengthIsOdd_IsCorrect()
        {
            var bits = new BitArray(new[] { true, false, true });
            var expected = new BitArray(new[] { true, false, true });
            Assert.AreEqual(expected, bits.ReverseBitArray());
        }

        [Test]
        public void XOR_BitArray_IsCorrect()
        {
            var bits = new BitArray(new[] { true, true, false, false });
            var subkey = new BitArray(new[] { true, false, true, false });
            var expected = new BitArray(new[] { false, true, true, false });
            Assert.AreEqual(expected, bits.XOR_BitArray(subkey));
        }

        [Test]
        public void Roundn_ShouldReturnCorrectBitArray()
        {
            // Arrange
            var bits = BitArrayExtensions.BitArrayFromBinaryString("11110000101010101111000010101010");

            // Act
            bits = bits.RoundFunction();

            // Assert
            var expected =
                BitArrayExtensions.BitArrayFromBinaryString("011110100001010101010101011110100001010101010101");
            Assert.That(bits, Is.EqualTo(expected), bits.ToBinaryString() + "\n" + expected.ToBinaryString());
        }

        [Test]
        public void Substitution_ShouldReturnCorrectBitArray()
        {
            // Arrange
            var bits = BitArrayExtensions.BitArrayFromBinaryString("011110100001010101010101011110100001010101010101");

            // Act
            bits = bits.Substitution();

            // Assert
            var expected = BitArrayExtensions.BitArrayFromBinaryString("01111101010100101001010001010110");
            Assert.That(bits, Is.EqualTo(expected), bits.ToBinaryString(4) + "\n" + expected.ToBinaryString(4));
        }
    }
}