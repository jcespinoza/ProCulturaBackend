namespace ProCultura.Data.Context
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using ProCultura.Data.Mappings.Events;
    using ProCultura.Data.Mappings.Security;
    using ProCultura.Data.Mappings.User;
    using ProCultura.Domain.Entities.Account;
    using ProCultura.Domain.Entities.Events;
    using ProCultura.Domain.Entities.Security;

    public class ProCulturaBackEndContext : DbContext
    {
        public IDbSet<UserEntity> UserModels { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }
        public IDbSet<Role> Roles { get; set; }

        public IDbSet<Event> Events { get; set; }

        public ProCulturaBackEndContext()
            : base("name=ProCulturaBackEndContext")
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
