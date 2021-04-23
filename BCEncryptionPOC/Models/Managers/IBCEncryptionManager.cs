namespace BCEncryptionPOC.Models.Managers
{
    public interface IBCEncryptionManager
    {
        string GetKeys();
        string EncryptText(string inEncryptString);
        string DecryptText(string cipherText);
        string EncryptText2(string inEncryptString);
        string DecryptText2(string cipherText);
    }
}