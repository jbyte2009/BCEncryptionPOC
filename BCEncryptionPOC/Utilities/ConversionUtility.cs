using System;

namespace BCEncryptionPOC.Utilities
{
    public class ConversionUtility
    {
        #region Method | ParseInt |

        public static int ParseInt(string inString)
        {
            int ans = 0;

            if (string.IsNullOrEmpty(inString))
                return ans;

            try
            {
                ans = int.Parse(inString);
            }
            catch
            {
                ans = 0;
            }

            return ans;
        }

        #endregion

        #region Method | ParseBool |

        public static bool ParseBool(string inString)
        {
            var ans = false;

            if (string.IsNullOrEmpty(inString))
                return ans;

            var inStringUpper = inString.ToUpper();

            if (inStringUpper == "Y" || inStringUpper == "N")
            {
                return inStringUpper == "Y";
            }

            if (inStringUpper == "1" || inStringUpper == "0")
            {
                return inStringUpper == "1";
            }

            if (inStringUpper == "YES" || inStringUpper == "NO")
            {
                return inStringUpper == "YES";
            }

            try
            {
                ans = bool.Parse(inString);
            }
            catch
            {
            }

            return ans;
        }

        #endregion

        #region Method | ParseLong |

        public static long ParseLong(string inString)
        {
            long ans = 0;

            if (string.IsNullOrEmpty(inString))
                return ans;

            try
            {
                ans = long.Parse(inString);
            }
            catch
            {
                ans = 0;
            }

            return ans;
        }

        #endregion

        #region Method | ParseDouble |

        public static double ParseDouble(string inString)
        {
            double ans = 0.0;

            if (string.IsNullOrEmpty(inString))
                return ans;

            try
            {
                ans = double.Parse(inString);
            }
            catch
            {
                ans = 0.0;
            }

            return ans;
        }

        #endregion

        #region Method | DecimalToInt |

        public static int DecimalToInt(string inString)
        {
            int ans = 0;

            if (string.IsNullOrEmpty(inString))
                return ans;

            try
            {
                ans = (int)Math.Round(ParseDecimal(inString), MidpointRounding.ToEven);
            }
            catch
            {
                ans = 0;
            }

            return ans;
        }

        #endregion

        #region Method | DecimalToMoney |

        public static string DecimalToMoney(string inString)
        {
            string ans;

            var decAmount = ParseDecimal(inString);

            try
            {
                ans = string.Format("{0:C}", decAmount).Replace("$", string.Empty);
            }
            catch
            {
                ans = "0.00";
            }

            return ans;
        }

        #endregion

        #region Method | ParseDecimal |

        public static decimal ParseDecimal(string inString)
        {
            decimal ans = 0.0m;

            if (string.IsNullOrEmpty(inString))
                return ans;

            try
            {
                ans = decimal.Parse(inString);
            }
            catch
            {
                ans = 0.0m;
            }

            return ans;
        }

        #endregion

        #region Method | ParseDateTime |

        public static DateTime ParseDateTime(string inString)
        {
            DateTime ans = DateTime.MinValue;

            if (string.IsNullOrEmpty(inString))
                return ans;

            try
            {
                ans = DateTime.Parse(inString);
            }
            catch
            {
                ans = DateTime.MinValue;
            }

            return ans;
        }

        #endregion

        #region Method | GetFullDateTimeString |

        public static string GetFullDateTimeString()
        {
            DateTime dt = DateTime.Now;

            var month = GetInterval(dt.Month);
            var day = GetInterval(dt.Day);
            var year = dt.Year.ToString();

            var hour = GetInterval(dt.Hour);
            var minute = GetInterval(dt.Minute);
            var second = GetInterval(dt.Second);
            var millisec = GetInterval(dt.Millisecond);

            return string.Format("{0}{1}{2}_{3}{4}{5}{6}", year, month, day, hour, minute, second, millisec);
        }

        #endregion

        #region Method | GetInterval |

        private static string GetInterval(int inInterval)
        {
            return (inInterval < 10) ? "0" + inInterval : inInterval.ToString();
        }

        #endregion
    }
}
