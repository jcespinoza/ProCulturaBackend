using System.Web.Script.Serialization;

namespace ProCultura.CrossCutting.Encryption
{
    public class AuthRequestFactory: IAuthRequestFactory
    {
        private readonly IEncryptionService encryptionService;

        public AuthRequestFactory(IEncryptionService _encryptionService)
        {
            encryptionService = _encryptionService;
        }

        public string BuildEncryptedRequest<T>(T request)
        {
            var jsonRequest = new JavaScriptSerializer().Serialize(request);
            var encryptedRequest = encryptionService.Encrypt(jsonRequest);
            return encryptedRequest;
        }

        public T BuildDecryptedRequest<T>(string encryptedToken)
        {
            var jsonString = encryptionService.Decrypt(encryptedToken);
            var decryptedAuthRequest = new JavaScriptSerializer().Deserialize<T>(jsonString);
            return decryptedAuthRequest;
        }
    }
}
