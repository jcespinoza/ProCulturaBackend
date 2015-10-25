namespace Procultura.Application.DTO.User
{
    public class AuthModel : ResponseBase
    {
        public string AccessToken { get; set; }
        public int Id { get; set; }
    }
}
