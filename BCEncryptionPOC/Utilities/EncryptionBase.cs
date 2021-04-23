using System.IO;
using System.Reflection;

namespace BCEncryptionPOC.Utilities
{
    public class EncryptionBase
    {
        #region Method | GetKey |

        protected static byte[] GetKey(string inKey)
        {
            return TokenKeyGenerator.GetByteKey(GetKeyText(inKey));
        }

        #endregion

        #region Method | GetKey (2) |

        protected static byte[] GetKey(string inLocation, string inFileName)
        {
            var EKey = string.Format(inFileName, inLocation);

            return TokenKeyGenerator.GetByteKey(File.ReadAllText(EKey));
        }

        #endregion

        #region Method | GetKeyText |

        private static string GetKeyText(string inKeyFileName)
        {
            var ans = string.Empty;

            var name = string.Format("BCEncryptionPOC.Data.{0}", inKeyFileName);

            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);

            using (var reader = new StreamReader(stream))
            {
                ans = reader.ReadToEnd();
            }

            return ans;
        }

        #endregion
    }
}