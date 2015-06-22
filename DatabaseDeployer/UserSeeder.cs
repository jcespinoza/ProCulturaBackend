using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DomainDrivenDatabaseDeployer;
using NHibernate;

namespace DatabaseDeployer
{
    class UserSeeder : IDataSeeder
    {
        readonly ISession _session;

        public UserSeeder(ISession session)
        {
            _session = session;
        }
        public void Seed()
        {
            var account = new User
            {
                Email = "test@test.com",
                Name = "Test Name",
                Password = "password",
                Role = "Admin"
            };
            SHA512 hashtool = SHA512.Create();
            byte[] pass1 = hashtool.ComputeHash(Encoding.UTF8.GetBytes(account.Password));
            string pass = BitConverter.ToString(pass1);
            byte[] salt1 = hashtool.ComputeHash(Encoding.UTF8.GetBytes(account.Email + account.Name));
            string salt = BitConverter.ToString(salt1);
            byte[] pass2 = hashtool.ComputeHash(Encoding.UTF8.GetBytes(pass.Replace("-", "") + salt.Replace("-", "")));
            string passFinal = BitConverter.ToString(pass2);
            account.Password = passFinal.Replace("-", "");
            account.Salt = salt.Replace("-", "");
            _session.Save(account);
        }
    }
}
