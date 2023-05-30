using NUnit.Framework;
using PKG;

namespace UnitTests
{
    [TestFixture]
    public class ElGamalTests
    {
        [SetUp]
        public void Setup()
        {
            elGamal = new ElGamal(p, x);
        }

        private readonly int p = 1009;
        private readonly int x = 532;
        private ElGamal elGamal;

        [Test]
        public void GeneratePrivateKey_ReturnsRandomInteger()
        {
            var privateKey1 = elGamal.GeneratePrivateKey();
            var privateKey2 = elGamal.GeneratePrivateKey();

            Assert.AreNotEqual(privateKey1, privateKey2);
        }

        [Test]
        public void Encrypt_ReturnsValidCiphertext()
        {
            var y = elGamal.GetPublicKey();
            var m = 10;

            var ciphertext = elGamal.Encrypt(m);

            Assert.IsTrue(ciphertext.Item1 > 0 && ciphertext.Item1 < p);
            Assert.IsTrue(ciphertext.Item2 > 0 && ciphertext.Item2 < p);
        }

        [Test]
        public void GenerateKey()
        {
            var y = elGamal.GetPublicKey();
            var y2 = elGamal.GetPublicKey();
            Assert.AreEqual(y, y2);
        }

        [Test]
        public void Decrypt_ReturnsCorrectPlaintext()
        {
            var y = elGamal.GetPublicKey();
            var m = 10;

            var ciphertext = elGamal.Encrypt(m);

            var decryptedMessage = elGamal.Decrypt(ciphertext);

            Assert.AreEqual(m, decryptedMessage);
        }
    }
}