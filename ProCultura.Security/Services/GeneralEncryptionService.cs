namespace ProCultura.Security.Services
{
    using System;
    using System.Globalization;
    using System.Text;

    public class GeneralEncryptionService
    {
        const string Key = "^#*GSdb38";
 
        public static string Encrypt(string plainText)
        {
            return BitConverter.ToString(Encoding.Default.GetBytes(Encryptamajig.AesEncryptamajig.Encrypt(plainText, Key))).Replace("-", "");
        }
 
        public static string Decrypt(string cipherText)
        {
            var deHexifiedString = "";
            for (var i = 0; i < cipherText.Length; i += 2)
            {
                deHexifiedString += (char)Int16.Parse(cipherText.Substring(i, 2), NumberStyles.AllowHexSpecifier);
            }
            return Encryptamajig.AesEncryptamajig.Decrypt(deHexifiedString, Key);
        }
    }
}