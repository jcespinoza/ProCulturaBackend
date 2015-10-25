namespace ProCultura.Data.Context
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Mappings.Events;
    using Mappings.Security;
    using Mappings.User;
    using Domain.Entities.Account;
    using Domain.Entities.Events;
    using Domain.Entities.Security;

    public class ProCulturaBackEndContext : DbContext
    {
        public IDbSet<UserEntity> UserModels { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }
        public IDbSet<Role> Roles { get; set; }

        public IDbSet<Event> Events { get; set; }

        public ProCulturaBackEndContext()
            : base("name=ProCulturaContext")
        {
            Database.SetInitializer(new ProCulturaContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new UserDetailEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UserRoleEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new RoleEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new EventEntityTypeConfiguration());
        }
    }
}
