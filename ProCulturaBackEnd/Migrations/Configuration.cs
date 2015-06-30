using ProCulturaBackEnd.Contexts;
using ProCulturaBackEnd.Entities;
using ProCulturaBackEnd.Services;

namespace ProCulturaBackEnd.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ProCulturaBackEndContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProCulturaBackEndContext context)
        {
            var userToSeed = new UserEntity()
            {
                Email = "jgpaz5@gmail.com",
                Name = "Gabriel",
                Id = 1,
                Password = "password",
                Role = 0
            };
            PasswordEncryptionService.Encrypt(userToSeed);
            context.UserModels.Add(userToSeed);
            context.SaveChanges();
        }
    }
}
