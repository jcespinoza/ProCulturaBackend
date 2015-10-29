using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProCultura.Data.Mappings.Events;
using ProCultura.Data.Mappings.Security;
using ProCultura.Data.Mappings.User;

namespace ProCultura.WebApiOwin.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<Privilege> Privileges { get; set; }
        public IDbSet<RolePrivilege> RolePrivileges { get; set; }

        public ApplicationDbContext()
            : base("ProCulturaContext", throwIfV1Schema: false)
        {
            Database.SetInitializer(new UsersInitializer());
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

        }
    }

    public class UsersInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Roles.Add(new IdentityRole
            {
                Id = "Administrator",
                Name = "Administrator",
            });
            context.Roles.Add(new IdentityRole
            {
                Id = "User",
                Name = "User",
            });
        }
    }
}