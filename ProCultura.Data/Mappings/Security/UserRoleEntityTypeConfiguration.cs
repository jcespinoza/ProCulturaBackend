namespace ProCultura.Data.Mappings.Security
{
    using System.Data.Entity.ModelConfiguration;

    using ProCultura.Domain.Entities.Security;

    public class UserRoleEntityTypeConfiguration: EntityTypeConfiguration<UserRole>
    {
        public UserRoleEntityTypeConfiguration()
        {
            HasKey(ur => new { ur.UserId, ur.RoleId });

            Property(ur => ur.UserId).HasColumnName("UserId");
            Property(ur => ur.RoleId).HasColumnName("RoleId");

            ToTable("UserRole", "Seguridad");
        }
    }
}
