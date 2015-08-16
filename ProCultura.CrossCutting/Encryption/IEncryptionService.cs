namespace ProCultura.CrossCutting.Encryption
{
    public interface IEncryptionService
    {
        string Encrypt(string plainText);

        string Decrypt(string cypherText);
    }
}
