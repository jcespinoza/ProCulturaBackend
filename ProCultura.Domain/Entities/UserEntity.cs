using System.ComponentModel.DataAnnotations.Schema;

namespace ProCultura.Domain.Entities
{
    public enum Role
    {
        Administrator = 5,
        User = 1
    }

    [Table("userdetails")]
    public class UserEntity
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public  string Email { get; set; }
        public  string Password { get; set; }
        public string Salt { get; set; }
        public Role Role { get; set; }
    }
}