
namespace BCEncryptionPOC.Models
{
    public class EncryptDTO : EncryptBase
    {
        public string TextToEncrypt { get; set; }
    }

    public class DecryptDTO : EncryptBase
    {
        public string DecryptText { get; set; }
    }

    public class EncryptBase
    {
        public string CredentialKey { get; set; }
        public string TokenKey { get; set; }
    }
}
