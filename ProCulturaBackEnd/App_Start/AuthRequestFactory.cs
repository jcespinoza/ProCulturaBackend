using System.Web.Script.Serialization;
using ProCulturaBackEnd.Models;

namespace ProCulturaBackEnd{
    public class AuthRequestFactory
    {
        public static string BuildEncryptedRequest(string email)
        {
            var request = new UserTokenModel
            {
                email = email
            };

            var jsonRequest = new JavaScriptSerializer().Serialize(request);
            var encryptedRequest = GeneralEncriptionService.Encrypt(jsonRequest);
            return encryptedRequest;
        }

        public static UserTokenModel BuildDecryptedRequest(string encryptedToken)
        {
            var jsonString = GeneralEncriptionService.Decrypt(encryptedToken);
            var decryptedAuthRequest = new JavaScriptSerializer().Deserialize<UserTokenModel>(jsonString);
            return decryptedAuthRequest;
        }
    }


}