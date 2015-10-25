using System.Collections.Generic;

namespace ProCultura.Domain.Entities.Security
{
    public class Role : IEntity
    {
        public string RoleId { get; set; }

        public string Name { get; set; }
        public int AuthorityLevel { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
