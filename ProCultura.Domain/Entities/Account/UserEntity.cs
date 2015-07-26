namespace ProCultura.Domain.Entities.Account
{
    using System.Collections.Generic;
    using System.Linq;

    using ProCultura.Domain.Entities.Security;

    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public bool IsAdmin()
        {
            return UserRoles != null && UserRoles.FirstOrDefault(ur => ur.RoleId == "Administrator") != null;
        }
    }
}