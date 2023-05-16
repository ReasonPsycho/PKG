using System;
using System.Collections;
using System.Diagnostics;

namespace PKG
{
    public class DES : ISymmetricCypher
    {
        public DES(Key key)
        {
            Keys = new[] { key };
            _random = new Random((int)DateTime.Now.Ticks);
        }

        public DES()
        {
            var bitString = "0000000100100011010001010110011110001001101010111100110111101111";
            var bitArray = BitArrayExtensions.BitArrayFromBinaryString(bitString);
            Keys = new[] { new Key(bitArray) };
            _random = new Random((int)DateTime.Now.Ticks);
        }

        public Key[] Keys { get; set; }
        public Random _random { get; set; }

        public BitArray CipherMessage(BitArray message)
        {
            // Perform initial permutation of the input message

            message = message.PermutateBitArray(BitArrayExtensions.IP); //TEST this

            Debug.WriteLine(message.ToBinaryString());


            message.SplitBitArray(out var leftHalf, out var rightHalf);


            for (var i = 0; i < 16; i++)
            {
                var originalRightHalf = (BitArray)rightHalf.Clone();

                rightHalf = rightHalf.RoundFunction();

                rightHalf = rightHalf.XOR_BitArray(Keys[0].Subkeys[i]);

                rightHalf = rightHalf.Substitution();

                rightHalf = rightHalf.PermutateBitArray(BitArrayExtensions.PBlock);

                rightHalf = leftHalf.XOR_BitArray(rightHalf);

                leftHalf = originalRightHalf;
            }

            message = BitArrayExtensions.JoinBitArray(rightHalf, leftHalf);

            // Perform final permutation of the message
            message = message.PermutateBitArray(BitArrayExtensions.FP);

            return message;
        }

        public BitArray DecipherMessage(BitArray message)
        {
            // Perform initial permutation of the input message

            message = message.PermutateBitArray(BitArrayExtensions.IP); //TEST this

            Console.WriteLine(message.ToBinaryString());


            message.SplitBitArray(out var leftHalf, out var rightHalf);


            for (var i = 15; i >= 0; i--)
            {
                var originalRightHalf = (BitArray)rightHalf.Clone();

                rightHalf = rightHalf.RoundFunction();

                rightHalf = rightHalf.XOR_BitArray(Keys[0].Subkeys[i]);

                rightHalf = rightHalf.Substitution();

                rightHalf = rightHalf.PermutateBitArray(BitArrayExtensions.PBlock);

                rightHalf = leftHalf.XOR_BitArray(rightHalf);

                leftHalf = originalRightHalf;
            }

            message = BitArrayExtensions.JoinBitArray(rightHalf, leftHalf);

            // Perform final permutation of the message
            message = message.PermutateBitArray(BitArrayExtensions.FP);

            return message;
        }
    }
}