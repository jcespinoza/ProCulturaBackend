namespace ProCultura.Domain.Entities.Security
{
    using System.Collections.Generic;

    public class Role
    {
        public string RoleId { get; set; }

        public string Name { get; set; }
        public int AuthorityLevel { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
