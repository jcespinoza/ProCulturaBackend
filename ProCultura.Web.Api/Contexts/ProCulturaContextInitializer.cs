using System.Data.Entity;
using System.Linq;

namespace ProCultura.Web.Api.Contexts
{
    using ProCultura.Domain.Entities;
    using ProCultura.Web.Api.Services;

    public class ProCulturaContextInitializer  : DropCreateDatabaseIfModelChanges<ProCulturaBackEndContext>
    {
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