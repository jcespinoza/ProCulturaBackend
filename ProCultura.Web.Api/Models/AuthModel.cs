namespace ProCultura.Web.Api.Models
{
    public class AuthModel : ResponseBase
    {
        public string AccessToken { get; set; }
        public int Id { get; set; }
    }
}
