using System;
using System.Globalization;
using System.Text;
using Encryptamajig;

namespace ProCultura.CrossCutting.Encryption
{
    public class GeneralEncryptionService : IEncryptionService
    {
        const string Key = "^#*GSdb38";

        public string Encrypt(string plainText)
        {
            return BitConverter.ToString(Encoding.Default.GetBytes(AesEncryptamajig.Encrypt(plainText, Key))).Replace("-", "");
        }

        public string Decrypt(string cipherText)
        {
            var deHexifiedString = "";
            for (var i = 0; i < cipherText.Length; i += 2)
            {
                deHexifiedString += (char)Int16.Parse(cipherText.Substring(i, 2), NumberStyles.AllowHexSpecifier);
            }
            return AesEncryptamajig.Decrypt(deHexifiedString, Key);
        }
    }
}
