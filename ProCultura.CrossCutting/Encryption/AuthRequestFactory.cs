namespace ProCultura.CrossCutting.Encryption
{
    using System.Web.Script.Serialization;
    public class AuthRequestFactory: IAuthRequestFactory
    {
        private readonly IEncryptionService encryptionService;

        public AuthRequestFactory(IEncryptionService _encryptionService)
        {
            //TODO: inject this dependency later
            encryptionService = new GeneralEncryptionService();
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
