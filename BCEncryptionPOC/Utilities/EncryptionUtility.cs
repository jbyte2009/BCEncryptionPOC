using BCEncryptionPOC.Init;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using System.Text;

namespace BCEncryptionPOC.Utilities
{
    public class EncryptionUtility : EncryptionBase
    {
        public static byte[] Key1 => GetKey(sInit.EncryptKey1);
        public static byte[] Key2 => GetKey(sInit.EncryptKey2);

        #region Method | Encrypt |

        public static string Encrypt(string inTextToEncrypt)
        {
            var encrypt = new Encryptor<TwofishEngine, Sha1Digest>(Encoding.UTF8, Key1, Key2);

            return encrypt.Encrypt(inTextToEncrypt);
        }

        #endregion

        #region Method | Encrypt (2) |

        public static string Encrypt(string inTextToEncrypt, string inLoc)
        {
            var encrypt = new Encryptor<TwofishEngine, Sha1Digest>(Encoding.UTF8, GetKey(inLoc, sInit.EncryptKeyFileName1), GetKey(inLoc, sInit.EncryptKeyFileName2));

            return encrypt.Encrypt(inTextToEncrypt);
        }

        #endregion

        #region Method | Decrypt |

        public static string Decrypt(string inCipher)
        {
            var encrypt = new Encryptor<TwofishEngine, Sha1Digest>(Encoding.UTF8, Key1, Key2);

            return encrypt.Decrypt(inCipher);
        }

        #endregion

        #region Method | Decrypt (2) |

        public static string Decrypt(string inCipher, string inLoc)
        {
            var encrypt = new Encryptor<TwofishEngine, Sha1Digest>(Encoding.UTF8, GetKey(inLoc, sInit.EncryptKeyFileName1), GetKey(inLoc, sInit.EncryptKeyFileName2));

            return encrypt.Decrypt(inCipher);
        }

        #endregion

        #region Method | AESEncrypt |

        public static string AESEncrypt(string inTextToEncrypt)
        {
            var encrypt = new Encryptor<AesEngine, Sha1Digest>(Encoding.UTF8, Key1, Key2);

            return encrypt.Encrypt(inTextToEncrypt);
        }

        #endregion

        #region Method | AESDecrypt |

        public static string AESDecrypt(string inCipher)
        {
            var encrypt = new Encryptor<AesEngine, Sha1Digest>(Encoding.UTF8, Key1, Key2);

            return encrypt.Decrypt(inCipher);
        }

        #endregion

        #region Method | GetTokenKey |

        public static string GetTokenKey()
        {
            return TokenKeyGenerator.GetKey();
        }

        #endregion
    }
}
