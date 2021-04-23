using System;
using System.Security.Cryptography;

namespace BCEncryptionPOC.Utilities
{
    public class TokenKeyGenerator
    {
        public static string GetKey()
        {
            byte[] tokenData = new byte[32];
            new RNGCryptoServiceProvider().GetBytes(tokenData);

            return Convert.ToBase64String(tokenData);
        }

        public static byte[] GetByteKey(string inKey)
        {
            return Convert.FromBase64String(inKey);
        }
    }
}
