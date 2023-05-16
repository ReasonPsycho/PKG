using System;
using System.Collections;
using System.IO;
using System.Text;

namespace PKG
{
    public interface ISymmetricCypher
    {
        Key[] Keys { get; set; }
        public BitArray CipherMessage(BitArray message);
        public BitArray DecipherMessage(BitArray message);
    }

    public static class SymmetricCypherExtensions
    {
        public static string EncodeString(this ISymmetricCypher symmetricCypher, string input)
        {
            // Determine padding length
            var padLength = 8 - input.Length % 8;
            var padValue = (byte)padLength;

            // Create padded message
            var paddedMessage = input;
            for (var i = 0; i < padLength; i++) paddedMessage += (char)padValue;

            // Calculate the amount of loops
            var n = paddedMessage.Length / 8;

            var output = "";
            for (var i = 0; i < n; i++)
            {
                var substring = paddedMessage.Substring(i * 8, 8);
                var bytes = Encoding.ASCII
                    .GetBytes(substring); //Maby error maby reverse idk XDD
                var bits = new BitArray(bytes);
                bits = symmetricCypher.CipherMessage(bits);
                bits.CopyTo(bytes, 0);
                output += bits.ToBinaryString();
            }

            return output;
        }

        public static string DecodeString(this ISymmetricCypher symmetricCypher, string input)
        {
            input = input.Replace(" ", "");
            // Calculate the amount of loops
            var n = input.Length / 64;

            var output = "";
            for (var i = 0; i < n; i++)
            {
                var substring = input.Substring(i * 64, 64);
                var bits = BitArrayExtensions.BitArrayFromBinaryString(substring);
                bits = symmetricCypher.DecipherMessage(bits);
                var bytes = new byte[8];
                bits.CopyTo(bytes, 0);
                output += Encoding.ASCII.GetString(bytes);
            }

            var isPadded = false;
            if (output.Length % 8 == 0 && output.Length > 0)
            {
                int lastByte = output[output.Length - 1];
                if (lastByte > 0 && lastByte <= 8) isPadded = true;
            }

            int padLength = output[output.Length - 1];

            if (!isPadded) return output;
            // Remove padding from message
            return output.Substring(0, output.Length - padLength);
        }

        public static string EncodeFile(this ISymmetricCypher symmetricCypher, string pathFrom, string pathTo)
        {
            // Open the file in binary mode
            var readStream = new FileStream(pathFrom, FileMode.Open, FileAccess.Read);
            if (File.Exists(pathTo)) File.Delete(pathTo);

            var writeStream = new FileStream(pathTo, FileMode.CreateNew, FileAccess.Write);

            // Create a BinaryReader to read from the file
            var binaryReader = new BinaryReader(readStream);
            var binaryWriter = new BinaryWriter(writeStream);

            for (var i = 0; i < binaryReader.BaseStream.Length; i += 8)
            {
                var byteArray = binaryReader.ReadBytes(8);
                if (byteArray.Length < 8) //That's adds unnecesery padding but i thinks it's key for now
                {
                    // Determine padding length
                    var padLength = 8 - byteArray.Length % 8;
                    var replaceBteArray = new byte[8];
                    byteArray.CopyTo(replaceBteArray, 0);
                    var padValue = (byte)padLength;

                    // Create padded byteArray
                    for (var j = 8 - padLength; j < 8; j++) replaceBteArray[j] = padValue;

                    byteArray = replaceBteArray;
                }

                var bitArray = new BitArray(byteArray);
                bitArray = symmetricCypher.CipherMessage(bitArray);
                var outArray = new byte[8];
                bitArray.CopyTo(outArray, 0);
                binaryWriter.Write(outArray);
            }


            //Close BinaryReader and writers
            binaryReader.Close();
            binaryWriter.Close();

            // Close the file streams
            readStream.Close();
            writeStream.Close();

            var output = "";

            using (var stream = new FileStream(pathTo, FileMode.Open))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    output = streamReader.ReadToEnd();
                }
            }

            return output;
        }

        public static string DecodeFile(this ISymmetricCypher symmetricCypher, string pathFrom, string pathTo)
        {
            // Open the file in binary mode
            var readStream = new FileStream(pathFrom, FileMode.Open, FileAccess.Read);

            if (File.Exists(pathTo)) File.Delete(pathTo);

            var writeStream = new FileStream(pathTo, FileMode.CreateNew, FileAccess.Write);

            // Create a BinaryReader to read from the file
            var binaryReader = new BinaryReader(readStream);
            var binaryWriter = new BinaryWriter(writeStream);


            byte[] byteArray;
            BitArray bitArray;
            byte[] outArray;
            for (var i = 0; i < binaryReader.BaseStream.Length - 8; i += 8)
            {
                byteArray = binaryReader.ReadBytes(8);
                bitArray = new BitArray(byteArray);
                bitArray = symmetricCypher.DecipherMessage(bitArray);
                outArray = new byte[8];
                bitArray.CopyTo(outArray, 0);
                binaryWriter.Write(outArray);
            }

            byteArray = binaryReader.ReadBytes(8);
            bitArray = new BitArray(byteArray);
            bitArray = symmetricCypher.DecipherMessage(bitArray);
            outArray = new byte[8];
            bitArray.CopyTo(outArray, 0);

            if (outArray.Length % 8 == 0 && outArray.Length > 0)
            {
                int lastByte = outArray[outArray.Length - 1];
                if (lastByte > 0 && lastByte <= 8)
                {
                    var tmpByteArray = new byte[8 - lastByte];
                    Array.Copy(outArray, 0, tmpByteArray, 0, 8 - lastByte);
                    outArray = tmpByteArray;
                }
            }


            binaryWriter.Write(outArray);

            //Close BinaryReader and writers
            binaryReader.Close();
            binaryWriter.Close();

            // Close the file streams
            readStream.Close();
            writeStream.Close();

            var output = "";

            using (var stream = new FileStream(pathTo, FileMode.Open))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    output = streamReader.ReadToEnd();
                }
            }

            return output;
        }
    }
}