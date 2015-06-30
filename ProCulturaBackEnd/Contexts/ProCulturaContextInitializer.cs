using System.Data.Entity;
using ProCulturaBackEnd.Entities;
using ProCulturaBackEnd.Models;
using ProCulturaBackEnd.Services;

namespace ProCulturaBackEnd.Contexts
{
    public class ProCulturaContextInitializer  : DropCreateDatabaseIfModelChanges<ProCulturaBackEndContext>
    {
        protected override void Seed(ProCulturaBackEndContext context)
        {
            var userToSeed = new UserEntity()
            {
                Email = "jgpaz5@gmail.com",
                Name = "Gabriel",
                Id = 1,
                Password = "password",
                Role = Role.Administrator
            };
            PasswordEncryptionService.Encrypt(userToSeed);
            context.UserModels.Add(userToSeed);
            context.SaveChanges();
        }
    }
}