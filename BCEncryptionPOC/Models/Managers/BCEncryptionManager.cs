using BCEncryptionPOC.Utilities;

namespace BCEncryptionPOC.Models.Managers
{
    public class BCEncryptionManager : IBCEncryptionManager
    {
        private EncryptDTO encDTO;
        private DecryptDTO decDTO;

        public string Loc => AppConfig.GetAppSetting("BC.KLoc");

        #region Method | GetKeys |

        public string GetKeys()
        {
            var key1 = EncryptionUtility.GetTokenKey();
            var key2 = EncryptionUtility.GetTokenKey();

            return key1 + " | " + key2;
        }

        #endregion

        #region Method | EncryptText |

        public string EncryptText(string inEncryptString)
        {
            encDTO = new EncryptDTO
            {
                CredentialKey = "",
                TextToEncrypt = inEncryptString,
                TokenKey = ""
            };

            var cipherText = EncryptionUtility.Encrypt(encDTO.TextToEncrypt);

            return cipherText;
        }

        #endregion

        #region Method | EncryptText2 |

        public string EncryptText2(string inEncryptString)
        {
            encDTO = new EncryptDTO
            {
                CredentialKey = "",
                TextToEncrypt = inEncryptString,
                TokenKey = ""
            };

            var cipherText = EncryptionUtility.Encrypt(encDTO.TextToEncrypt, Loc);

            return cipherText;
        }

        #endregion

        #region Method | DecryptText |

        public string DecryptText(string cipherText)
        {
            decDTO = new DecryptDTO
            {
                CredentialKey = "",
                DecryptText = cipherText,
                TokenKey = ""
            };

            var clearText = EncryptionUtility.Decrypt(decDTO.DecryptText);

            return clearText;
        }

        #endregion

        #region Method | DecryptText2 |

        public string DecryptText2(string cipherText)
        {
            decDTO = new DecryptDTO
            {
                CredentialKey = "",
                DecryptText = cipherText,
                TokenKey = ""
            };

            var clearText = EncryptionUtility.Decrypt(decDTO.DecryptText, Loc);

            return clearText;
        }

        #endregion
    }
}
