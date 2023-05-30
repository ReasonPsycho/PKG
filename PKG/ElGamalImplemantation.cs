using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PKG
{
    public class ElGamalImplemantation
    {
        public static string EncodeString(ElGamal elGamal, string input)
        {
            // Convert input to byte array
            var bytes = Encoding.ASCII.GetBytes(input);

            // Calculate the amount of loops
            var n = bytes.Length;

            var output = new StringBuilder();
            for (var i = 0; i < n; i++)
            {
                var a = elGamal.Encrypt(bytes[i]);
                output.Append($"{a.Item1},{a.Item2},");
            }

            return output.ToString();
        }

        public static string DecodeString(ElGamal elGamal, string input)
        {
            // Split input into ciphertext tuples
            var ciphertextTuples = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Calculate the amount of loops
            var n = ciphertextTuples.Length / 2;

            var outputBytes = new List<byte>();
            for (var i = 0; i < n * 2; i += 2)
                if (int.TryParse(ciphertextTuples[i], out var a) && int.TryParse(ciphertextTuples[i + 1], out var b))
                {
                    var byte1 = elGamal.Decrypt(Tuple.Create(a, b));

                    outputBytes.Add((byte)byte1);
                }

            // Convert output byte array to string
            return Encoding.ASCII.GetString(outputBytes.ToArray());
        }


        public static string EncodeFile(ElGamal elGamal, string pathFrom, string pathTo)
        {
            // Open the input file in binary mode
            var readStream = new FileStream(pathFrom, FileMode.Open, FileAccess.Read);

            // Create a new file to write output to
            var writeStream = new FileStream(pathTo, FileMode.Create);
            var binaryWriter = new BinaryWriter(writeStream);

            int bytesRead;
            var buffer = new byte[1];
            while ((bytesRead = readStream.Read(buffer, 0, 1)) > 0)
            {
                // Encrypt the buffer and write the ciphertext to output file
                var a = elGamal.Encrypt(buffer[0]);
                binaryWriter.Write(a.Item1);
                binaryWriter.Write(a.Item2);
            }

            // Close streams
            readStream.Close();
            binaryWriter.Close();
            writeStream.Close();

            return File.ReadAllText(pathTo);
        }

        public static string DecodeFile(ElGamal elGamal, string pathFrom, string pathTo)
        {
            // Open the input file in binary mode
            var readStream = new FileStream(pathFrom, FileMode.Open, FileAccess.Read);

            // Create a new file to write output to
            var writeStream = new FileStream(pathTo, FileMode.Create);
            var binaryWriter = new BinaryWriter(writeStream);

            var buffer = new byte[8];
            while (readStream.Read(buffer, 0, 8) > 0)
            {
                // Decrypt the ciphertext and write the decrypted bytes to output file
                var a = Tuple.Create(BitConverter.ToInt32(buffer, 0), BitConverter.ToInt32(buffer, 4));
                binaryWriter.Write((byte)elGamal.Decrypt(a));
            }

            // Close streams
            readStream.Close();
            binaryWriter.Close();
            writeStream.Close();

            return File.ReadAllText(pathTo);
        }
    }
}