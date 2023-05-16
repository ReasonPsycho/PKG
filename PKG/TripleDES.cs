using System.Collections;

namespace PKG
{
    public class TripleDES : ISymmetricCypher
    {
        public TripleDES(Key[] keys)
        {
            Keys = keys;
        }

        public Key[] Keys { get; set; }

        public BitArray CipherMessage(BitArray message)
        {
            DES[] deses = { new DES(Keys[0]), new DES(Keys[1]), new DES(Keys[2]) };
            message = deses[0].CipherMessage(message);
            message = deses[1].DecipherMessage(message);
            message = deses[2].CipherMessage(message);
            return message;
        }

        public BitArray DecipherMessage(BitArray message)
        {
            DES[] deses = { new DES(Keys[0]), new DES(Keys[1]), new DES(Keys[2]) };
            message = deses[2].DecipherMessage(message);
            message = deses[1].CipherMessage(message);
            message = deses[0].DecipherMessage(message);
            return message;
        }
    }
}