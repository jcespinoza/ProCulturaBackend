using System.ComponentModel.DataAnnotations;

namespace ProCulturaBackEnd.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El correo es requerido")]
        [DataType(DataType.EmailAddress)]  
        public string Email { get; set; }
        [Required(ErrorMessage = "La contrasenia es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}