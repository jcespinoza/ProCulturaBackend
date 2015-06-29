using System.Data.Entity;

namespace ProCulturaBackEnd.Models
{
    public class ProCulturaContextInitializer  : DropCreateDatabaseIfModelChanges<ProCulturaBackEndContext>
    {
        protected override void Seed(ProCulturaBackEndContext context)
        {
            var userToSeed = new UserModel()
            {
                Email = "jgpaz5@gmail.com",
                Name = "Gabriel",
                Id = 1,
                Password = "password"
            };
            PasswordEncryptionService.Encrypt(userToSeed);
            context.UserModels.Add(userToSeed);
            context.SaveChanges();
        }
    }
}