﻿using System.Web.Script.Serialization;

namespace ProCultura.Web.Api.Services{
    using ProCultura.Web.Api.Models;
    using ProCultura.Security.Services;

    public class AuthRequestFactory
    {
        public static string BuildEncryptedRequest(string email)
        {
            var request = new UserTokenModel
            {
                Email = email
            };

            var jsonRequest = new JavaScriptSerializer().Serialize(request);
            var encryptedRequest = GeneralEncryptionService.Encrypt(jsonRequest);
            return encryptedRequest;
        }

        public static UserTokenModel BuildDecryptedRequest(string encryptedToken)
        {
            var jsonString = GeneralEncryptionService.Decrypt(encryptedToken);
            var decryptedAuthRequest = new JavaScriptSerializer().Deserialize<UserTokenModel>(jsonString);
            return decryptedAuthRequest;
        }
    }


}