using System.ComponentModel.DataAnnotations;

namespace ProCultura.Web.Api.Models
{
    using ProCultura.Domain.Entities;

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