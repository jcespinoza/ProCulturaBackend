using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ProCulturaBackEnd.Models;

namespace ProCulturaBackEnd.App_Start
{
    public class AuthRequestFactory
    {
        public static string BuildEncryptedRequest(string email)
        {
            var request = new UserTokenModel
            {
                email = email
            };

            var jsonRequest = new JavaScriptSerializer().Serialize(request);
            var encryptedRequest = Encripter.Encrypt(jsonRequest);
            return encryptedRequest;
        }

        public static UserTokenModel BuildDecryptedRequest(string encryptedToken)
        {
            var jsonString = Encripter.Decrypt(encryptedToken);
            var decryptedAuthRequest = new JavaScriptSerializer().Deserialize<UserTokenModel>(jsonString);
            return decryptedAuthRequest;
        }
    }
}