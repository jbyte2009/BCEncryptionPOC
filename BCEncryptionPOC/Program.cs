using BCEncryptionPOC.Models.Managers;
using System;

namespace BCEncryptionPOC
{
    class Program
    {
        private static IBCEncryptionManager manager;

        private static string KeyText;
        private static string Option;
        private static string TextToEncrypt;
        private static string cipherText;
        private static string clearText;

        static void Main(string[] args)
        {
            manager = new BCEncryptionManager();

            PromptUser();
        }

        #region Method | PromptUser |

        private static void PromptUser()
        {
            var bContinue = true;
            var i = 0;
            var Opt = string.Empty;

            Console.Clear();

            while (bContinue)
            {
                Console.WriteLine("Enter ['E' = Encrypt] or ['K' = Keys] or ['X' = Exit]:  ");
                Option = Console.ReadLine();

                Opt = Option.ToUpper();

                i++;

                if(Opt == "E" || Opt == "K" || Opt == "X")
                {
                    bContinue = false;

                    if (Opt == "E")
                    {
                        Console.WriteLine("Enter Text to Encrypt:  ");
                        TextToEncrypt = Console.ReadLine();
                    }
                }

                if(i >= 2)
                {
                    bContinue = false;
                    Opt = "X";
                }
            }

            switch (Opt)
            {
                case "K":
                    GetKeys();
                    KeyDisplay();
                    PromptUser();
                    break;
                case "E":
                    EncryptTest(TextToEncrypt);
                    DecryptTest(cipherText);
                    EncryptDisplay();
                    PromptUser();
                    break;
                case "X":
                    Console.WriteLine("Good Bye.");
                    Console.ReadKey();
                    break;
                default:
                    EncryptTest(TextToEncrypt);
                    DecryptTest(cipherText);
                    EncryptDisplay();
                    PromptUser();
                    break;
            }
        }

        #endregion

        #region Method | KeyDisplay |

        private static void KeyDisplay()
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(string.Format("Keys: {0}", KeyText));
            Console.ReadKey();
        }

        #endregion

        #region Method | EncryptDisplay |

        private static void EncryptDisplay()
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(string.Format("Text To Encrypt: {0}", TextToEncrypt));
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(string.Format("Encrypt Text: {0}", cipherText));
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(string.Format("Decrypt Text: {0}", clearText));
            Console.ReadKey();
        }

        #endregion

        #region Method | DecryptTest |

        private static void DecryptTest(string cipherText)
        {
            clearText = manager.DecryptText(cipherText);
        }

        #endregion

        #region Method | EncryptTest |

        private static void EncryptTest(string inTextToEncrypt)
        {
            cipherText = manager.EncryptText(inTextToEncrypt);
        }

        #endregion

        #region Method | GetKeys |

        private static void GetKeys()
        {
            KeyText = manager.GetKeys();
        }

        #endregion
    }
}
