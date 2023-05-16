using System;
using System.Collections;

namespace PKG
{
    public class Key
    {
        private static readonly int[] Pc1Table =
        {
            56, 48, 40, 32, 24, 16, 8, 0,
            57, 49, 41, 33, 25, 17, 9, 1,
            58, 50, 42, 34, 26, 18, 10, 2,
            59, 51, 43, 35, 62, 54, 46, 38,
            30, 22, 14, 6, 61, 53, 45, 37,
            29, 21, 13, 5, 60, 52, 44, 36,
            28, 20, 12, 4, 27, 19, 11, 3
        };

        private static readonly int[] Pc2Table =
        {
            13, 16, 10, 23, 0, 4, 2, 27,
            14, 5, 20, 9, 22, 18, 11, 3,
            25, 7, 15, 6, 26, 19, 12, 1,
            40, 51, 30, 36, 46, 54, 29, 39,
            50, 44, 32, 47, 43, 48, 38, 55,
            33, 52, 45, 41, 49, 35, 28, 31
        };

        private static readonly int[] shifts =
            { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        public BitArray[] ReversedSubkeys;
        public BitArray[] Subkeys;

        public Key(BitArray input)
        {
            Subkeys = new BitArray[16];
            ReversedSubkeys = new BitArray[16];

            // Step 1-2: Split(1) the permute(2) key into left and right halves
            var left_half = new BitArray(28);
            var right_half = new BitArray(28);
            for (var i = 0; i < 28; i++)
            {
                left_half[i] = input[Pc1Table[i]];
                right_half[i] = input[Pc1Table[i + 28]];
            }

            // Step 3: Generate subkeys for each round
            for (var i = 0; i < 16; i++)
            {
                // Apply a circular shift to each half
                var shift = shifts[i];
                left_half = left_half.LeftShift(shift); //TODO somwhere here
                right_half = right_half.LeftShift(shift);

                // Combine the halves and permute using the PC-2 table
                var CD = new BitArray(56);
                for (var j = 0; j < 28; j++)
                {
                    CD[j] = left_half[j];
                    CD[j + 28] = right_half[j];
                }

                var subkey = new BitArray(48);
                for (var j = 0; j < 48; j++)
                    subkey[j] = CD[Pc2Table[j]];

                Subkeys[i] = subkey;
            }

            for (var i = 0; i < 16; i++) ReversedSubkeys[i] = Subkeys[15 - i];
        }


        public Key(long longInput)
        {
            var input = new BitArray(BitConverter.GetBytes(longInput));

            Subkeys = new BitArray[16];
            ReversedSubkeys = new BitArray[16];

            // Step 1-2: Split(1) the permute(2) key into left and right halves
            var left_half = new BitArray(28);
            var right_half = new BitArray(28);
            for (var i = 0; i < 28; i++)
            {
                left_half[i] = input[Pc1Table[i]];
                right_half[i] = input[Pc1Table[i + 28]];
            }

            // Step 3: Generate subkeys for each round
            for (var i = 0; i < 16; i++)
            {
                // Apply a circular shift to each half
                var shift = shifts[i];
                left_half = left_half.LeftShift(shift); //TODO somwhere here
                right_half = right_half.LeftShift(shift);

                // Combine the halves and permute using the PC-2 table
                var CD = new BitArray(56);
                for (var j = 0; j < 28; j++)
                {
                    CD[j] = left_half[j];
                    CD[j + 28] = right_half[j];
                }

                var subkey = new BitArray(48);
                for (var j = 0; j < 48; j++)
                    subkey[j] = CD[Pc2Table[j]];

                Subkeys[i] = subkey;
            }

            for (var i = 0; i < 16; i++) ReversedSubkeys[i] = Subkeys[15 - i];
        }

        public Key()
        {
            var newKey = new Key(GenrateRandomKeyInput());
            Subkeys = newKey.Subkeys;
            ReversedSubkeys = newKey.ReversedSubkeys;
        }

        public override string ToString()
        {
            var result = "";

            for (var i = 0; i < 16; i++) result += Subkeys[i].ToBinaryString() + "\n";

            return result;
        }

        public static long GenrateRandomKeyInput() //TODO fix this bullshit
        {
            var ticks = DateTime.Now.Ticks;
            var rnd = new Random((int)(ticks & 0xffffffffL) | (int)(ticks >> 32));
            var randomValue = (uint)rnd.Next();
            return Math.Abs((ticks << 32) | randomValue);
        }
    }
}