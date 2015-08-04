namespace ProCultura.Domain.Entities.Security
{
    using ProCultura.Domain.Entities.Account;

    public class UserRole : IEntity
    {
        public int UserId { get; set; }

        public string RoleId { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual Role Role { get; set; }
    }
}
