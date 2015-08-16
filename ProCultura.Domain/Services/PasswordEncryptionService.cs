namespace ProCultura.Domain.Services
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    using ProCultura.Domain.Entities.Account;

    public static class PasswordEncryptionService
    {
        public static bool CheckPassword(UserEntity user, string password)
        {
            var hashtool = SHA512.Create();
            var pass1 = hashtool.ComputeHash(Encoding.UTF8.GetBytes(password));
            var pass = BitConverter.ToString(pass1);
            var pass2 = hashtool.ComputeHash(Encoding.UTF8.GetBytes(pass.Replace("-", "") + user.Salt));
            var passFinal = BitConverter.ToString(pass2).Replace("-", "");
            return user.Password.Equals(passFinal);
        }

        public static void Encrypt(UserEntity account)
        {
            var hashtool = SHA512.Create();
            var pass1 = hashtool.ComputeHash(Encoding.UTF8.GetBytes(account.Password));
            var pass = BitConverter.ToString(pass1);
            var salt1 = hashtool.ComputeHash(Encoding.UTF8.GetBytes(account.Email + account.Name));
            var salt = BitConverter.ToString(salt1);
            var pass2 = hashtool.ComputeHash(Encoding.UTF8.GetBytes(pass.Replace("-", "") + salt.Replace("-", "")));
            var passFinal = BitConverter.ToString(pass2);
            account.Password = passFinal.Replace("-", "");
            account.Salt = salt.Replace("-", "");
        }
    }
}
