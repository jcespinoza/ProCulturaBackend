using System.ComponentModel.DataAnnotations;

namespace ProCultura.Web.Api.Models
{
    using Procultura.Application.DTO;

    public class RegisterModel : RequestBase
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]  
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}