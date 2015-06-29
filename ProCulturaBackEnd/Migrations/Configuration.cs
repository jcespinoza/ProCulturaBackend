using ProCulturaBackEnd.Models;

namespace ProCulturaBackEnd.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProCulturaBackEnd.Models.ProCulturaBackEndContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProCulturaBackEndContext context)
        {
            var UserToSeed = new UserModel()
            {
                Email = "jgpaz5@gmail.com",
                Name = "Gabriel",
                Id = 1,
                Password = GeneralEncriptionService.Encrypt("password")
            };

            context.UserModels.Add(UserToSeed);
            context.SaveChanges();
        }
        
    }
}
