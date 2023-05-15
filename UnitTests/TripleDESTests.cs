using NUnit.Framework;
using PKG;

namespace UnitTests
{
    [TestFixture]
    public class TripleDESTests
    {
        [Test]
        public void EncriptionTest()
        {
            var bitString = "0000000100100011010001010110011110001001101010111100110111101111";
            var toTest = BitArrayExtensions.BitArrayFromBinaryString(bitString);
            toTest.ReverseBitArray(8);
            var key = new Key(toTest);
            var message = BitArrayExtensions.BitArrayFromBinaryString(bitString);
            message = DES.CipherMessage(message, key);
            var bitTests =
                BitArrayExtensions.BitArrayFromBinaryString(
                    "0101011011001100000010011110011111001111110111000100110011101111");
            Assert.AreEqual(bitTests, message, bitTests.ToBinaryString(8) + "\n" + message.ToBinaryString(8));
        }

        [Test]
        public void DecriptionTest()
        {
            var bitString = "0000000100100011010001010110011110001001101010111100110111101111";
            var toTest = BitArrayExtensions.BitArrayFromBinaryString(bitString);
            toTest.ReverseBitArray(8);
            var key = new Key(toTest);
            var des = new DES();
            var message = BitArrayExtensions.BitArrayFromBinaryString(bitString);
            message = DES.CipherMessage(message, key);
            message = DES.DecipherMessage(message, key);

            Assert.AreEqual(toTest, message, toTest.ToBinaryString(8) + "\n" + message.ToBinaryString(8));
        }
    }
}