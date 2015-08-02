namespace Procultura.Application.DTO.User
{
    using Procultura.Application.DTO;

    public class AuthModel : ResponseBase
    {
        public string AccessToken { get; set; }
        public int Id { get; set; }
    }
}
