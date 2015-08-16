namespace Procultura.Application.DTO.User
{
    using Procultura.Application.DTO;

    public class LoginModel: RequestBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}