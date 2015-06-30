using System.ComponentModel.DataAnnotations;
using ProCulturaBackEnd.Entities;

namespace ProCulturaBackEnd.Models
{
    public class UserModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [ScaffoldColumn(false)]
        public Role Role { get; set; }
    }
}