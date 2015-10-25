namespace ProCultura.Data.Context
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Domain.Entities.Account;
    using Domain.Entities.Events;
    using Domain.Entities.Security;
    using Domain.Services;

    public class ProCulturaContextInitializer : CreateDatabaseIfNotExists<ProCulturaBackEndContext>
    {
        protected override void Seed(ProCulturaBackEndContext context)
        {
            var adminUserRole = new UserRole() { RoleId = "Administrator", UserId = 1 };
            var adminRole = new Role() { RoleId = "Administrator", Active = true, AuthorityLevel = 42 };
            var userRole = new Role() { RoleId = "User", Active = true, AuthorityLevel = 10 };

            var userToSeed = new UserEntity
            {
                Email = "admin@procultura.com",
                Name = "Admin",
                Id = 1,
                Password = "admin"
            };

            PasswordEncryptionService.Encrypt(userToSeed);

            var userEntity = context.UserModels.FirstOrDefault(x => x.Id == 1);
            var adminUserRoleEntity =
                context.UserRoles.FirstOrDefault(ur => ur.UserId == userToSeed.Id && ur.RoleId == adminRole.RoleId);
            var adminRoleEntity =
                context.Roles.FirstOrDefault(ur => ur.RoleId == adminRole.RoleId);
            var userRoleEntity =
                context.Roles.FirstOrDefault(ur => ur.RoleId == userRole.RoleId);

            if (userEntity != null)
            {
                context.Entry(userEntity).CurrentValues.SetValues(userToSeed);
            }
            else
            {
                context.UserModels.Add(userToSeed);
            }

            if (adminUserRoleEntity != null)
            {
                context.Entry(adminUserRoleEntity).CurrentValues.SetValues(adminUserRole);
            }
            else
            {
                context.UserRoles.Add(adminUserRole);
            }

            if (adminRoleEntity != null)
            {
                context.Entry(adminRoleEntity).CurrentValues.SetValues(adminRole);
            }
            else
            {
                context.Roles.Add(adminRole);
            }

            if (userRoleEntity != null)
            {
                context.Entry(userRoleEntity).CurrentValues.SetValues(userRole);
            }
            else
            {
                context.Roles.Add(userRole);
            }

            SeedEvents(context);

            context.SaveChanges();
        }

        private void SeedEvents(ProCulturaBackEndContext context)
        {
            var eventOne = new Event
                               {
                                   EventId = 1,
                                   Name = "Polache's Wild Concert",
                                   BeginDate = new DateTime(2015, 09, 12),
                                   EndDate = new DateTime(2015,09, 12),
                                   ShortDescription = "Enjoy listening to Polaches master pieces this September 12th",
                                   ImageUrl =
                                       "http://www.hondudiario.com/sites/default/files/dt.common.streams.StreamServer_55.jpg",
                                   Location = "Morazan Stadium, San Pedro Sula",
                                   LongDescription = "This concert will be the best CATRACHADA in the Honduran history of concerts. Bring your \"punta\" shoes and suit to dance at the \"Pan de Coco\" rhythm!",
                                   Status = "SOLD"
                               };
            context.Events.Add(eventOne);
        }
    }
}