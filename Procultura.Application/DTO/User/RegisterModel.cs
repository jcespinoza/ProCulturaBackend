namespace Procultura.Application.DTO.User
{
    using Procultura.Application.DTO;

    public class RegisterModel : RequestBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}