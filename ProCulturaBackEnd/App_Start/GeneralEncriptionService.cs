namespace ProCulturaBackEnd
{
    public class GeneralEncriptionService
    {
        const string Key = "^#*GSdb38";
 
        public static string Encrypt(string plainText)
        {
            return Encryptamajig.AesEncryptamajig.Encrypt(plainText, Key);
        }
 
        public static string Decrypt(string cipherText)
        {
            return Encryptamajig.AesEncryptamajig.Decrypt(cipherText, Key);
        }
    }
}