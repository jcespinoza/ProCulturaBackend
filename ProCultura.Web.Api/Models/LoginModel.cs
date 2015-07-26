using System.ComponentModel.DataAnnotations;

namespace ProCultura.Web.Api.Models
{

    public class LoginModel: RequestModel
    {
        [Required(ErrorMessage = "El correo es requerido")]
        [DataType(DataType.EmailAddress)]  
        public string Email { get; set; }
        [Required(ErrorMessage = "La contrasenia es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}