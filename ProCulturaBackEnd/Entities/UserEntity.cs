using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProCulturaBackEnd.Models;

namespace ProCulturaBackEnd.Entities
{
    public enum Role
    {
        Administrator = 5,
        User = 1
    }

    [Table("userdetails")]
    public class UserEntity : ResponseModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public  string Name { get; set; }
        [Required]
        public  string Email { get; set; }
        [Required]
        public  string Password { get; set; }
        [ScaffoldColumn(false)]
        public string Salt { get; set; }
        [ScaffoldColumn(false)]
        public Role Role { get; set; }
    }
}