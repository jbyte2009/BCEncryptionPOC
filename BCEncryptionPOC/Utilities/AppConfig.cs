using System;
using System.Configuration;

namespace BCEncryptionPOC.Utilities
{
    public class AppConfig
    {
        #region | Method | GetConnectionString |

        public static string GetConnectionString(string sKeyIn)
        {
            string ans = "";

            ConnectionStringSettings css = ConfigurationManager.ConnectionStrings[sKeyIn];

            if (css != null)
            {
                ans = css.ConnectionString;
            }

            return (ans);
        }

        #endregion

        #region | Method | GetAppSetting |

        public static string GetAppSetting(string sKeyIn)
        {
            string ans = string.Empty;

            try
            {
                ans = ConfigurationManager.AppSettings[sKeyIn];
            }
            catch (Exception e)
            {
                string sErrMsg = e.Message;
            }

            return (ans);
        }

        #endregion

        #region | Method | GetIntProperty |

        public static int GetIntProperty(string propertyName)
        {
            return ConversionUtility.ParseInt(GetAppSetting(propertyName));
        }

        #endregion

        #region | Method | GetBoolProperty |

        public static bool GetBoolProperty(string propertyName)
        {
            return ConversionUtility.ParseBool(GetAppSetting(propertyName));
        }

        #endregion
    }
}
