using System;
using System.Collections;
using System.IO;
using NUnit.Framework;
using PKG;

namespace UnitTests
{
    [TestFixture]
    public class EgdeCaseTest
    {
        private static readonly string bitString = "0000000100100011010001010110011110001001101010111100110111101111";
        private static readonly BitArray toTest = BitArrayExtensions.BitArrayFromBinaryString(bitString);
        private static readonly Key TestKey = new Key(toTest);

        [Test]
        public void Case_9_163()
        {
            var LoremIpsumPDF_Orginal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Orginal.pdf");

            // Act

            var buffer = new byte[8]; // create a byte array to hold the 8 bytes
            using (var stream = new FileStream(LoremIpsumPDF_Orginal, FileMode.Open)) // open the file
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    stream.Seek(2560, SeekOrigin.Begin); // set the file pointer to byte 2560
                    buffer = binaryReader.ReadBytes(8); // read 8 bytes into the buffer
                }
            }

            var testArray = new BitArray(buffer);
            var encodedArray = DES.CipherMessage(testArray, TestKey);
            var decodedArray = DES.DecipherMessage(encodedArray, TestKey);
            var outByte = new byte[8];
            decodedArray.CopyTo(outByte, 0);
            Assert.AreEqual(buffer, outByte,
                testArray.ToBinaryString(8) + "\n" + decodedArray.ToBinaryString(8));
        }
    }
}