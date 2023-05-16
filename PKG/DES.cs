using System;
using System.Collections;

namespace PKG
{
    public class DES : ISymmetricCypher
    {
        public DES(Key key)
        {
            Keys = new[] { key };
        }

        public Key[] Keys { get; set; }

        public BitArray CipherMessage(BitArray message)
        {
            // Perform initial permutation of the input message

            message = message.PermutateBitArray(BitArrayExtensions.IP); //TEST this

            Console.WriteLine(message.ToBinaryString());


            BitArray left_half;
            BitArray right_half;

            message.SplitBitArray(out left_half, out right_half);


            for (var i = 0; i < 16; i++)
            {
                var original_right_half = (BitArray)right_half.Clone();

                right_half = right_half.RoundFunction();

                right_half = right_half.XOR_BitArray(Keys[0].Subkeys[i]);

                right_half = right_half.Substitution();

                right_half = right_half.PermutateBitArray(BitArrayExtensions.PBlock);

                right_half = left_half.XOR_BitArray(right_half);

                left_half = original_right_half;
            }

            message = BitArrayExtensions.JoinBitArray(right_half, left_half);

            // Perform final permutation of the message
            message = message.PermutateBitArray(BitArrayExtensions.FP);

            return message;
        }

        public BitArray DecipherMessage(BitArray message)
        {
            // Perform initial permutation of the input message

            message = message.PermutateBitArray(BitArrayExtensions.IP); //TEST this

            Console.WriteLine(message.ToBinaryString());


            BitArray left_half;
            BitArray right_half;

            message.SplitBitArray(out left_half, out right_half);


            for (var i = 15; i >= 0; i--)
            {
                var original_right_half = (BitArray)right_half.Clone();

                right_half = right_half.RoundFunction();

                right_half = right_half.XOR_BitArray(Keys[0].Subkeys[i]);

                right_half = right_half.Substitution();

                right_half = right_half.PermutateBitArray(BitArrayExtensions.PBlock);

                right_half = left_half.XOR_BitArray(right_half);

                left_half = original_right_half;
            }

            message = BitArrayExtensions.JoinBitArray(right_half, left_half);

            // Perform final permutation of the message
            message = message.PermutateBitArray(BitArrayExtensions.FP);

            return message;
        }
    }
}