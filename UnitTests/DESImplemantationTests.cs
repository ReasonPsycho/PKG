using System;
using System.Collections;
using System.IO;
using NUnit.Framework;
using PKG;

namespace UnitTests
{
    [TestFixture]
    public class DESImplemantationTests
    {
        private readonly string orginal = "Hello, world!";

        private static readonly string bitString = "0000000100100011010001010110011110001001101010111100110111101111";
        private static readonly BitArray toTest = BitArrayExtensions.BitArrayFromBinaryString(bitString);
        private static readonly Key TestKey = new Key(toTest);

        [Test]
        public void EncodeDecodeStringTest()
        {
            // Act
            var encoded = DESImplemantation.EncodeString(orginal, TestKey);
            var output = DESImplemantation.DecodeString(encoded, TestKey);
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
            DESImplemantation.EncodeFile(orginalFile, encodedFile, TestKey);
            DESImplemantation.DecodeFile(encodedFile, decodedFile, TestKey);


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
        public void LoremIpsumTXT()
        {
            var LoremIpsumTXT_Orginal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Orginal.txt");
            var LoremIpsumTXT_Encoded = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Encoded.txt");
            var LoremIpsumTXT_Decoded = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Decoded.txt");

            // Act
            DESImplemantation.EncodeFile(LoremIpsumTXT_Orginal, LoremIpsumTXT_Encoded, TestKey);
            DESImplemantation.DecodeFile(LoremIpsumTXT_Encoded, LoremIpsumTXT_Decoded, TestKey);


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
            DESImplemantation.EncodeFile(LoremIpsumPDF_Orginal, LoremIpsumPDF_Encoded, TestKey);
            DESImplemantation.DecodeFile(LoremIpsumPDF_Encoded, LoremIpsumPDF_Decoded, TestKey);


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
            DESImplemantation.EncodeFile(LoremIpsumPNG_Orginal, LoremIpsumPNG_Encoded, TestKey);
            DESImplemantation.DecodeFile(LoremIpsumPNG_Encoded, LoremIpsumPNG_Decoded, TestKey);


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