using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace PKG
{
    public static class BitArrayExtensions
    {
        public static readonly int[] IP =
        {
            57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39,
            31, 23, 15, 7, 56, 48, 40, 32, 24, 16, 8, 0, 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6
        };

        public static readonly int[] FP =
        {
            39, 7, 47, 15, 55, 23, 63, 31,
            38, 6, 46, 14, 54, 22, 62, 30,
            37, 5, 45, 13, 53, 21, 61, 29,
            36, 4, 44, 12, 52, 20, 60, 28,
            35, 3, 43, 11, 51, 19, 59, 27,
            34, 2, 42, 10, 50, 18, 58, 26,
            33, 1, 41, 9, 49, 17, 57, 25,
            32, 0, 40, 8, 48, 16, 56, 24
        };

        public static readonly int[] PBlock =
        {
            15, 6, 19, 20, 28, 11, 27, 16,
            0, 14, 22, 25, 4, 17, 30, 9,
            1, 7, 23, 13, 31, 26, 2, 8,
            18, 12, 29, 5, 21, 10, 3, 24
        };

        public static readonly int[][] S_BOXES =
        {
            new[]
            {
                14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7,
                0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8,
                4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0,
                15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13
            },
            new[]
            {
                15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10,
                3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5,
                0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15,
                13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9
            },
            new[]
            {
                10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8,
                13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1,
                13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7,
                1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12
            },
            new[]
            {
                7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15,
                13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9,
                10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4,
                3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14
            },
            new[]
            {
                2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9,
                14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6,
                4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14,
                11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3
            },
            new[]
            {
                12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11,
                10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8,
                9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6,
                4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13
            },
            new[]
            {
                4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1,
                13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6,
                1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2,
                6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12
            },
            new[]
            {
                13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7,
                1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2,
                7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8,
                2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11
            }
        };

        private static readonly int[] E =
        {
            31, 0, 1, 2, 3, 4,
            3, 4, 5, 6, 7, 8,
            7, 8, 9, 10, 11, 12,
            11, 12, 13, 14, 15, 16,
            15, 16, 17, 18, 19, 20,
            19, 20, 21, 22, 23, 24,
            23, 24, 25, 26, 27, 28,
            27, 28, 29, 30, 31, 0
        };

        private static readonly int[] EInverse =
        {
            9, 17, 23, 31,
            13, 28, 2, 18,
            24, 16, 30, 6,
            26, 20, 10, 1,
            8, 14, 25, 3,
            4, 29, 11, 19,
            32, 12, 22, 7,
            5, 27, 15, 21
        };

        public static BitArray LeftShift(this BitArray bits, int shift)
        {
            var result = new BitArray(bits.Length);
            for (var i = 0; i < bits.Length; i++)
            {
                var j = (i + shift) % bits.Length;
                result[i] = bits[j];
            }

            return result;
        }

        public static BitArray RoundFunction(this BitArray bits)
        {
            var orginal = new BitArray(bits);
            bits = new BitArray(48);

            for (var i = 0; i < 48; i++) bits[i] = orginal[E[i]];

            return bits;
        }

        public static BitArray InverseRoundFunction(this BitArray bits)
        {
            var result = new BitArray(48);

            for (var i = 0; i < 48; i++) result[i] = bits[EInverse[i]];

            return result;
        }

        public static BitArray ReverseBitArray(this BitArray bits)
        {
            var len = bits.Length;
            var reversedBits = new BitArray(len);

            for (var i = 0; i < len; i++) reversedBits[i] = bits[len - i - 1];

            return reversedBits;
        }


        public static BitArray ReverseBitArray(this BitArray bits, int arraySize)
        {
            var len = bits.Length;
            var reversedBits = new BitArray(len);

            for (var i = 0; i < len; i += arraySize)
            for (var j = 0; j < arraySize && i + j < len; j++)
                reversedBits[i + j] = bits[i + arraySize - j - 1];

            return reversedBits;
        }

        public static BitArray XOR_BitArray(this BitArray bits, BitArray subkey)
        {
            for (var i = 0; i < bits.Count; i++) bits[i] ^= subkey[i];

            return bits;
        }

        public static BitArray Substitution(this BitArray bits)
        {
            var result = new BitArray(32);
            var row = new BitArray(2);
            var column = new BitArray(4);

            for (var i = 0; i < 8; i++)
            {
                var currentSBox = S_BOXES[i];
                row[0] = bits[i * 6];
                row[1] = bits[i * 6 + 5];
                column[0] = bits[i * 6 + 1];
                column[1] = bits[i * 6 + 2];
                column[2] = bits[i * 6 + 3];
                column[3] = bits[i * 6 + 4];
                var rowValue = row.getIntFromBitArray(2);
                var columnValue = column.getIntFromBitArray(4);
                var index = rowValue * 16 + columnValue;
                var takeAway = new BitArray(4);
                var take = currentSBox[index];
                takeAway = new BitArray(new[] { take });
                takeAway = takeAway.ReverseBitArray(4);
                result[i * 4 + 0] = takeAway[0];
                result[i * 4 + 1] = takeAway[1];
                result[i * 4 + 2] = takeAway[2];
                result[i * 4 + 3] = takeAway[3];
            }

            return result;
        }


        private static int getIntFromBitArray(this BitArray bitArray, int lenght)
        {
            var result = 0;

            for (var i = lenght - 1; i >= 0; i--)
                if (bitArray[i])
                    result += (int)Math.Pow(2, lenght - 1 - i);


            return result;
        }

        public static string ToBinaryString(this BitArray bits, int addSpaces = 0)
        {
            var result = "";
            if (addSpaces == 0)
            {
                for (var i = 0; i < bits.Count; i++)
                    result += bits[i] ? "1" : "0";
            }
            else
            {
                var j = 0;
                for (var i = 0; i < bits.Count; i++)
                {
                    result += bits[i] ? "1" : "0";
                    j++;
                    if (j >= addSpaces)
                    {
                        result += " ";
                        j = 0;
                    }
                }
            }


            return result;
        }

        public static BitArray PermutateBitArray(this BitArray bits, int[] table)
        {
            var original = new BitArray(bits);
            for (var i = 0; i < original.Length; i++)
                bits[i] = original[table[i]];
            return bits;
        }

        public static BitArray SplitBitArray(this BitArray bits, out BitArray a, out BitArray b)
        {
            a = new BitArray(bits.Length / 2);
            b = new BitArray(bits.Length / 2);
            for (var i = 0; i < bits.Length / 2; i++)
            {
                a[i] = bits[i];
                b[i] = bits[i + bits.Length / 2];
            }

            return bits;
        }

        public static BitArray JoinBitArray(BitArray a, BitArray b)
        {
            var bits = new BitArray(a.Length + b.Length);
            for (var i = 0; i < bits.Length / 2; i++)
            {
                bits[i] = a[i];
                bits[i + bits.Length / 2] = b[i];
            }

            return bits;
        }


        //UNTESTETD

        public static BitArray GetFromHex(string hexString)
        {
            var sbyteArray = Enumerable.Range(0, hexString.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToSByte(hexString.Substring(x, 2), 16))
                .ToArray();
            var bytes = sbyteArray.Select(s => unchecked((byte)s)).ToArray();
            var output = new BitArray(bytes);
            output = output.ReverseBitArray(8);
            return output;
        }

        public static BitArray BitArrayFromBinaryString(string bitString)
        {
            bitString = bitString.Replace(" ", "");
            var length = bitString.Length;
            var bits = new bool[length];
            for (var i = 0; i < length; i++) bits[i] = bitString[i] == '1';
            return new BitArray(bits);
        }

        public static string BitArrayToString(BitArray bits)
        {
            var bytes = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(bytes, 0);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}