using System;
using System.IO;
using NUnit.Framework;
using PKG;

namespace UnitTests
{
    [TestFixture]
    public class ElGamalImplementationTests
    {
        [SetUp]
        public void Setup()
        {
            elGamal = new ElGamal(p, x);
        }

        private readonly int p = 569;
        private readonly int x = 532;
        private ElGamal elGamal;

        [Test]
        public void EncodeDecodeString()
        {
            // Arrange
            var orginal = "hello world";

            // Act
            var encodedString = ElGamalImplemantation.EncodeString(elGamal, orginal);
            var decodedString = ElGamalImplemantation.DecodeString(elGamal, encodedString);

            // Assert
            Assert.AreEqual(orginal, decodedString); // Encrypted string should be different from original
        }

        [Test]
        public void LoremIpsumTXT()
        {
            var LoremIpsumTXT_Orginal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Orginal.txt");
            var LoremIpsumTXT_Encoded = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Encoded.txt");
            var LoremIpsumTXT_Decoded = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lorem Ipsum Decoded.txt");

            // Act
            ElGamalImplemantation.EncodeFile(elGamal, LoremIpsumTXT_Orginal, LoremIpsumTXT_Encoded);
            ElGamalImplemantation.DecodeFile(elGamal, LoremIpsumTXT_Encoded, LoremIpsumTXT_Decoded);


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
            ElGamalImplemantation.EncodeFile(elGamal, LoremIpsumPDF_Orginal, LoremIpsumPDF_Encoded);
            ElGamalImplemantation.DecodeFile(elGamal, LoremIpsumPDF_Encoded, LoremIpsumPDF_Decoded);


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
            ElGamalImplemantation.EncodeFile(elGamal, LoremIpsumPNG_Orginal, LoremIpsumPNG_Encoded);
            ElGamalImplemantation.DecodeFile(elGamal, LoremIpsumPNG_Encoded, LoremIpsumPNG_Decoded);


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