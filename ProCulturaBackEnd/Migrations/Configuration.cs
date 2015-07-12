using System.Linq;
using ProCulturaBackEnd.Contexts;
using ProCulturaBackEnd.Services;

namespace ProCulturaBackEnd.Migrations
{
    using System.Data.Entity.Migrations;

    using ProCultura.Domain.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<ProCulturaBackEndContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProCulturaBackEndContext context)
        {
            var userToSeed = new UserEntity
            {
                Email = "admin@proculturabackend.com",
                Name = "Admin",
                Id = 1,
                Password = "adminpassword",
                Role = Role.Administrator
            };
            PasswordEncryptionService.Encrypt(userToSeed);
            var entity = context.UserModels.FirstOrDefault(x => x.Id == 1);

            if (entity != null)
                context.Entry(entity).CurrentValues.SetValues(userToSeed);
            else
                context.UserModels.Add(userToSeed);
            context.SaveChanges();
        }
    }
}
