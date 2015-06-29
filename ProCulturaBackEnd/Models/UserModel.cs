using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProCulturaBackEnd.Models
{
    [Table("userdetails")]
    public class UserModel : ResponseModel
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
        public int Role { get; set; }
    }
}