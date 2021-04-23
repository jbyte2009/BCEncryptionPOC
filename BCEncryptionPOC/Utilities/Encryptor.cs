using System;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace BCEncryptionPOC.Utilities
{
    public sealed class Encryptor<TBlockCipher, TDigest>
        where TBlockCipher : IBlockCipher, new()
        where TDigest : IDigest, new()
    {
        private readonly Encoding encoding;

        private IBlockCipher blockCipher;

        private BufferedBlockCipher cipher;

        private HMac mac;

        private readonly byte[] key;

        #region | Constructor |

        public Encryptor(Encoding encoding, byte[] key, byte[] macKey)
        {
            this.encoding = encoding;
            this.key = key;
            Init(macKey, new Pkcs7Padding());
        }

        public Encryptor(Encoding encoding, byte[] key, byte[] macKey, IBlockCipherPadding padding)
        {
            this.encoding = encoding;
            this.key = key;
            Init(macKey, padding);
        }

        #endregion

        #region Method | Init |

        private void Init(byte[] macKey, IBlockCipherPadding padding)
        {
            blockCipher = new CbcBlockCipher(new TBlockCipher());
            cipher = new PaddedBufferedBlockCipher(blockCipher, padding);
            mac = new HMac(new TDigest());
            mac.Init(new KeyParameter(macKey));
        }

        #endregion

        #region Method | Encrypt |

        public string Encrypt(string plain)
        {
            return Convert.ToBase64String(EncryptBytes(plain));
        }

        #endregion

        #region Method | EncryptBytes |

        public byte[] EncryptBytes(string plain)
        {
            byte[] input = encoding.GetBytes(plain);

            var iv = GenerateIV();

            var cipher2 = BouncyCastleCrypto(true, input, new ParametersWithIV(new KeyParameter(key), iv));
            byte[] message = CombineArrays(iv, cipher2);

            mac.Reset();
            mac.BlockUpdate(message, 0, message.Length);
            byte[] digest = new byte[mac.GetUnderlyingDigest().GetDigestSize()];
            mac.DoFinal(digest, 0);

            var result = CombineArrays(digest, message);
            return result;
        }

        #endregion

        #region Method | DecryptBytes |

        public byte[] DecryptBytes(byte[] bytes)
        {
            // split the digest into component parts
            var digest = new byte[mac.GetUnderlyingDigest().GetDigestSize()];
            var message = new byte[bytes.Length - digest.Length];
            var iv = new byte[blockCipher.GetBlockSize()];
            var cipher2 = new byte[message.Length - iv.Length];

            Buffer.BlockCopy(bytes, 0, digest, 0, digest.Length);
            Buffer.BlockCopy(bytes, digest.Length, message, 0, message.Length);
            if (!IsValidHMac(digest, message))
            {
                throw new CryptoException();
            }

            Buffer.BlockCopy(message, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(message, iv.Length, cipher2, 0, cipher2.Length);

            byte[] result = BouncyCastleCrypto(false, cipher2, new ParametersWithIV(new KeyParameter(key), iv));
            return result;
        }

        #endregion

        #region Method | Decrypt |

        public string Decrypt(byte[] bytes)
        {
            return encoding.GetString(DecryptBytes(bytes));
        }

        #endregion

        #region Method | Decrypt (2) |

        public string Decrypt(string cipher2)
        {
            return Decrypt(Convert.FromBase64String(cipher2));
        }

        #endregion

        #region Method | IsValidHMac |

        private bool IsValidHMac(byte[] digest, byte[] message)
        {
            mac.Reset();
            mac.BlockUpdate(message, 0, message.Length);
            byte[] computed = new byte[mac.GetUnderlyingDigest().GetDigestSize()];
            mac.DoFinal(computed, 0);

            return AreEqual(digest, computed);
        }

        #endregion

        #region Method | AreEqual |

        private static bool AreEqual(byte[] digest, byte[] computed)
        {
            if (digest.Length != computed.Length)
            {
                return false;
            }

            int result = 0;
            for (int i = 0; i < digest.Length; i++)
            {
                // compute equality of all bytes before returning.
                //   helps prevent timing attacks: 
                //   https://codahale.com/a-lesson-in-timing-attacks/
                result |= digest[i] ^ computed[i];
            }

            return result == 0;
        }

        #endregion

        #region Method | BouncyCastleCrypto |

        private byte[] BouncyCastleCrypto(bool forEncrypt, byte[] input, ICipherParameters parameters)
        {
            cipher.Init(forEncrypt, parameters);

            return cipher.DoFinal(input);
        }

        #endregion

        #region Method | GenerateIV |

        private byte[] GenerateIV()
        {
            var provider = new RNGCryptoServiceProvider();

            // 1st block
            byte[] result = new byte[blockCipher.GetBlockSize()];
            provider.GetBytes(result);

            return result;
        }

        #endregion

        #region Method | CombineArrays |

        private static byte[] CombineArrays(byte[] source1, byte[] source2)
        {
            byte[] result = new byte[source1.Length + source2.Length];
            Buffer.BlockCopy(source1, 0, result, 0, source1.Length);
            Buffer.BlockCopy(source2, 0, result, source1.Length, source2.Length);

            return result;
        }

        #endregion
    }
}
