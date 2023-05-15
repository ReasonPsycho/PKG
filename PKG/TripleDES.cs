using System.Collections;

namespace PKG
{
    public class TripleDES
    {
        public BitArray CipherMessage(BitArray message, Key key0, Key key1, Key key2)
        {
            message = DES.CipherMessage(message, key0);
            message = DES.DecipherMessage(message, key1);
            message = DES.CipherMessage(message, key2);
            return message;
        }

        public BitArray DecipherMessage(BitArray message, Key key0, Key key1, Key key2)
        {
            message = DES.DecipherMessage(message, key0);
            message = DES.CipherMessage(message, key1);
            message = DES.DecipherMessage(message, key2);
            return message;
        }
    }
}