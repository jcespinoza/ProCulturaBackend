namespace ProCultura.Data.Context
{
    using System.Data.Entity;
    using System.Linq;

    using ProCultura.Domain.Entities.Account;
    using ProCultura.Domain.Entities.Security;
    using ProCultura.Domain.Services;

    public class ProCulturaContextInitializer : DropCreateDatabaseIfModelChanges<ProCulturaBackEndContext>
    {
        protected override void Seed(ProCulturaBackEndContext context)
        {
            var adminUserRole = new UserRole() { RoleId = "Administrator", UserId = 1 };
            var adminRole = new Role() { RoleId = "Administrator", Active = true, AuthorityLevel = 42 };
            var userRole = new Role() { RoleId = "User", Active = true, AuthorityLevel = 10 };

            var userToSeed = new UserEntity
            {
                Email = "admin@proculturabackend.com",
                Name = "Admin",
                Id = 1,
                Password = "adminpassword"
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

            context.SaveChanges();
        }
    }
}