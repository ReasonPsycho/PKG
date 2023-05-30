using System;
using System.Numerics;

namespace PKG
{
    public class ElGamal
    {
        private readonly int g;

        private readonly int p;
        private readonly Random rand = new Random();
        public readonly int x;
        private readonly int y;

        //Constructor for ElGamal object
        public ElGamal(int p, int x)
        {
            this.p = p; //large prime number
            if (x != 0)
                this.x = x;
            else
                this.x = GeneratePrivateKey();

            g = ElGamalGenerator.FindGenerator(p); //generator of the multiplicative group mod p
            y = GetPublicKey();
        }

        //Generate private and public keys
        public int GeneratePrivateKey()
        {
            // Generate a random integer between 1 and p-2
            return rand.Next(1, p - 2);
        }

        //Get public key
        public int GetPublicKey()
        {
            return (int)BigInteger.ModPow(g, x, p);
        }

        //Encryption function (takes public key y and plaintext message m)
        public Tuple<int, int> Encrypt(int m)
        {
            var k = GeneratePrivateKey();
            var a = (int)BigInteger.ModPow(g, k, p);
            var b = m * (int)BigInteger.ModPow(y, k, p) % p;

            return Tuple.Create(a, b);
        }

        //Decryption function (takes ciphertext [a, b])
        public int Decrypt(Tuple<int, int> c)
        {
            var a = c.Item1;
            var b = c.Item2;

            // Calculate shared secret
            var s = BigInteger.ModPow(a, x, p);

            // Calculate modular inverse of s using extended Euclidean algorithm

            // Calculate decrypted message
            var decryptedMessage = (int)BigInteger.ModPow(a, p - 1 - x, p) * b % p;
            return decryptedMessage;
        }
    }
}