namespace Procultura.Application.DTO.User
{
    public class LoginModel: RequestBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}