using System;
using System.Collections;

namespace PKG
{
    public class DES
    {
        public static BitArray CipherMessage(BitArray message, Key key)
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

                right_half = right_half.XOR_BitArray(key.Subkeys[i]);

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

        public static BitArray DecipherMessage(BitArray message, Key key)
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

                right_half = right_half.XOR_BitArray(key.Subkeys[i]);

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