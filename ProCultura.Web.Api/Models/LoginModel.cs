﻿using System.ComponentModel.DataAnnotations;

namespace ProCultura.Web.Api.Models
{
    using Procultura.Application.DTO;

    public class LoginModel: RequestBase
    {
        [Required(ErrorMessage = "El correo es requerido")]
        [DataType(DataType.EmailAddress)]  
        public string Email { get; set; }
        [Required(ErrorMessage = "La contrasenia es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}