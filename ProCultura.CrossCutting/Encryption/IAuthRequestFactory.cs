namespace ProCultura.CrossCutting.Encryption
{
    public interface IAuthRequestFactory
    {
        string BuildEncryptedRequest<T>(T request);

        T BuildDecryptedRequest<T>(string encryptedToken);
    }
}
