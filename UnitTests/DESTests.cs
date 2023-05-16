using System;
using System.Collections;
using System.IO;
using NUnit.Framework;
using PKG;

namespace UnitTests
{
    [TestFixture]
    public class DESTests
    {
        [SetUp]
        public void CreateDES()
        {
            des = new DES(TestKey);
        }

        private readonly string orginal = "Hello, world!";
        private static readonly string bitString = "0000000100100011010001010110011110001001101010111100110111101111";
        private static readonly BitArray toTest = BitArrayExtensions.BitArrayFromBinaryString(bitString);
        private static readonly Key TestKey = new Key(toTest);
        private ISymmetricCypher des;

        [Test]
        public void EncriptionTest()
        {
            var bitString = "0000000100100011010001010110011110001001101010111100110111101111";
            var toTest = BitArrayExtensions.BitArrayFromBinaryString(bitString);
            toTest.ReverseBitArray(8);
            var key = new Key(toTest);
            var message = BitArrayExtensions.BitArrayFromBinaryString(bitString);
            ISymmetricCypher symmetricCypher = new DES(key);
            message = symmetricCypher.CipherMessage(message);
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
            ISymmetricCypher symmetricCypher = new DES(key);
            var message = BitArrayExtensions.BitArrayFromBinaryString(bitString);
            message = symmetricCypher.CipherMessage(message);
            message = symmetricCypher.DecipherMessage(message);

            Assert.AreEqual(toTest, message, toTest.ToBinaryString(8) + "\n" + message.ToBinaryString(8));
        }

        [Test]
        public void EncodeDecodeStringTest()
        {
            // Act
            var encoded = des.EncodeString(orginal);
            var output = des.DecodeString(encoded);
            // Assert
            Assert.AreEqual(orginal, output);
        }

        [Test]
        public void EncodeDecodeFileTest()
        {
            // Arrange
            var orginalFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "orginalFile.txt");
            var encodedFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "encodedFile.txt");
            var decodedFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "decodedFile.txt");
            File.WriteAllText(orginalFile, orginal);

            // Act
            des.EncodeFile(orginalFile, encodedFile);
            des.DecodeFile(encodedFile, decodedFile);


            // Assert
            Assert.That(File.Exists(orginalFile));
            Assert.That(File.Exists(encodedFile));
            Assert.That(File.Exists(decodedFile));
            Console.Write(File.ReadAllBytes(orginalFile));
            Console.Write(File.ReadAllBytes(decodedFile));
            Assert.AreEqual(File.ReadAllBytes(orginalFile), File.ReadAllBytes(decodedFile));

            // Clean up
            File.Delete(orginalFile);
            File.Delete(encodedFile);
            File.Delete(decodedFile);
        }

        [Test]
        public void EncodeDecodeStringTestFull()
        {
            var loremIpsum =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas molestie ullamcorper tellus, eget convallis augue dapibus quis.";
            // Act
            var encoded = des.EncodeString(loremIpsum);
            var output = des.DecodeString(encoded);
            // Assert
            Assert.AreEqual(loremIpsum, output);
        }

        [Test]
        public void LoremIpsumTXT()
        {
            var LoremIpsumTXT_Orginal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Orginal.txt");
            var LoremIpsumTXT_Encoded = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Encoded.txt");
            var LoremIpsumTXT_Decoded = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Decoded.txt");

            // Act
            des.EncodeFile(LoremIpsumTXT_Orginal, LoremIpsumTXT_Encoded);
            des.DecodeFile(LoremIpsumTXT_Encoded, LoremIpsumTXT_Decoded);


            //Assert
            Assert.That(File.Exists(LoremIpsumTXT_Orginal));
            Assert.That(File.Exists(LoremIpsumTXT_Encoded));
            Assert.That(File.Exists(LoremIpsumTXT_Decoded));
            Console.Write(File.ReadAllBytes(LoremIpsumTXT_Orginal));
            Console.Write(File.ReadAllBytes(LoremIpsumTXT_Decoded));
            Assert.AreEqual(File.ReadAllBytes(LoremIpsumTXT_Orginal), File.ReadAllBytes(LoremIpsumTXT_Decoded));

            // Clean up
            File.Delete(LoremIpsumTXT_Encoded);
            File.Delete(LoremIpsumTXT_Decoded);
        }

        [Test]
        public void LoremIpsumPDF()
        {
            var LoremIpsumPDF_Orginal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Orginal.pdf");
            var LoremIpsumPDF_Encoded = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Encoded.pdf");
            var LoremIpsumPDF_Decoded = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Decoded.pdf");

            // Act
            des.EncodeFile(LoremIpsumPDF_Orginal, LoremIpsumPDF_Encoded);
            des.DecodeFile(LoremIpsumPDF_Encoded, LoremIpsumPDF_Decoded);


            //Assert
            Assert.That(File.Exists(LoremIpsumPDF_Orginal));
            Assert.That(File.Exists(LoremIpsumPDF_Encoded));
            Assert.That(File.Exists(LoremIpsumPDF_Decoded));
            Console.Write(File.ReadAllBytes(LoremIpsumPDF_Orginal));
            Console.Write(File.ReadAllBytes(LoremIpsumPDF_Decoded));
            Assert.AreEqual(File.ReadAllBytes(LoremIpsumPDF_Orginal), File.ReadAllBytes(LoremIpsumPDF_Decoded));

            // Clean up
            File.Delete(LoremIpsumPDF_Encoded);
            File.Delete(LoremIpsumPDF_Decoded);
        }

        [Test]
        public void LoremIpsumPNG()
        {
            var LoremIpsumPNG_Orginal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Orginal.png");
            var LoremIpsumPNG_Encoded = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Encoded.png");
            var LoremIpsumPNG_Decoded = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Decoded.png");

            // Act
            des.EncodeFile(LoremIpsumPNG_Orginal, LoremIpsumPNG_Encoded);
            des.DecodeFile(LoremIpsumPNG_Encoded, LoremIpsumPNG_Decoded);


            //Assert
            Assert.That(File.Exists(LoremIpsumPNG_Orginal));
            Assert.That(File.Exists(LoremIpsumPNG_Encoded));
            Assert.That(File.Exists(LoremIpsumPNG_Decoded));
            Console.Write(File.ReadAllBytes(LoremIpsumPNG_Orginal));
            Console.Write(File.ReadAllBytes(LoremIpsumPNG_Decoded));
            Assert.AreEqual(File.ReadAllBytes(LoremIpsumPNG_Orginal), File.ReadAllBytes(LoremIpsumPNG_Decoded));

            // Clean up
            File.Delete(LoremIpsumPNG_Encoded);
            File.Delete(LoremIpsumPNG_Decoded);
        }
    }
}