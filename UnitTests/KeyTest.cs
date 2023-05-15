using System;
using NUnit.Framework;
using PKG;

namespace UnitTests
{
    [TestFixture]
    public class KeyTest
    {
        [Test]
        public void GenerateKey()
        {
            var bitString =
                "0000000100100011010001010110011110001001101010111100110111101111"; //0000000100100011010001010110011110001001101010111100110111101111
            var bitArray = BitArrayExtensions.BitArrayFromBinaryString(bitString);
            var key = new Key(bitArray);
            Console.WriteLine(key.ToString());

            string[] stringToBitArray =
            {
                "000010110000001001100111100110110100100110100101",
                "011010011010011001011001001001010110101000100110",
                "010001011101010010001010101101000010100011010010",
                "011100101000100111010010101001011000001001010111",
                "001111001110100000000011000101111010011011000010",
                "001000110010010100011110001111001000010101000101",
                "011011000000010010010101000010101110010011000110",
                "010101111000100000111000011011001110010110000001",
                "110000001100100111101001001001101011100000111001",
                "100100011110001100000111011000110001110101110010",
                "001000010001111110000011000011011000100100111010",
                "011100010011000011100101010001010101110001010100",
                "100100011100010011010000010010011000000011111100",
                "010101000100001110110110100000011101110010001101",
                "101101101001000100000101000010100001011010110101",
                "110010100011110100000011101110000111000000110010"
            };
            for (var i = 0; i < stringToBitArray.Length; i++)
            {
                var testSubKey = BitArrayExtensions.BitArrayFromBinaryString(stringToBitArray[i]);
                Assert.AreEqual(testSubKey, key.Subkeys[i],
                    "i: " + i + "\n" + testSubKey.ToBinaryString() + "\n" + key.Subkeys[i].ToBinaryString());
            }
        }

        [Test]
        public void GenerateRandomKey()
        {
            var key1 = new Key(DESImplemantation.GenrateRandomKeyInput());
            var key2 = new Key(DESImplemantation.GenrateRandomKeyInput());
            Assert.AreNotEqual(key1, key2);
        }
    }
}