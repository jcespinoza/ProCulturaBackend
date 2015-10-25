using System.Data.Entity.ModelConfiguration;
using ProCultura.Domain.Entities.Account;

namespace ProCultura.Data.Mappings.User
{
    public class UserDetailEntityTypeConfiguration: EntityTypeConfiguration<UserEntity>
    {
        public UserDetailEntityTypeConfiguration()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name).HasMaxLength(100);


            // Table & Column Mappings
            ToTable("UserDetail", "Account");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Name).HasColumnName("Email");
            Property(t => t.Name).HasColumnName("Password");
            Property(t => t.Name).HasColumnName("Salt");

            //Relations
            HasMany(u => u.UserRoles).WithRequired(r => r.User).HasForeignKey(u => u.UserId);
        }
    }
}
