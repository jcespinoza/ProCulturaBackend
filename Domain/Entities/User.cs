using System;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Entities
{
    public class User : IEntity
    {
        public User()
        {
            Archived = false;
        }
        public virtual long Id { get; set; }
        public virtual bool Archived { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string Salt { get; set; }
        public virtual string Role { get; set; }
        public virtual void Archive()
        {
            Archived = true;
        }
        public virtual void Activate()
        {
            Archived = false;
        }
        public virtual bool CheckPassword(string password)
        {
            SHA512 hashtool = SHA512.Create();
            byte[] pass1 = hashtool.ComputeHash(Encoding.UTF8.GetBytes(password));
            string pass = BitConverter.ToString(pass1);
            byte[] pass2 = hashtool.ComputeHash(Encoding.UTF8.GetBytes(pass.Replace("-", "") + Salt));
            string passFinal = BitConverter.ToString(pass2).Replace("-", "");
            if (Password.Equals(passFinal))
                return true;
            else
                return false;
        }
    }
}
