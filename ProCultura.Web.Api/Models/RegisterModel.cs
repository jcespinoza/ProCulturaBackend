using System.ComponentModel.DataAnnotations;

namespace ProCultura.Web.Api.Models
{

    public class RegisterModel : RequestModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]  
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password should be the same")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}