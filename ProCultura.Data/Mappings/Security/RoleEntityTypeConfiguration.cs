using System.Data.Entity.ModelConfiguration;
using ProCultura.Domain.Entities.Security;

namespace ProCultura.Data.Mappings.Security
{
    public class RoleEntityTypeConfiguration: EntityTypeConfiguration<Role>
    {
        public RoleEntityTypeConfiguration()
        {
            //Primary Key
            HasKey(r => r.RoleId);

            //Properties
            Property(r => r.RoleId).HasColumnName("RoleId");
            Property(r => r.Name).HasColumnName("Name");
            Property(r => r.AuthorityLevel).HasColumnName("AuthorityLevel");
            Property(r => r.Active).HasColumnName("Active");

            //Table and Relations
            ToTable("Role", "Seguridad");
            HasMany(r => r.UserRoles).WithRequired(ru => ru.Role);
        }
    }
}
