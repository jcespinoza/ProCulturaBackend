namespace ProCultura.Web.Api.Models
{
    using Procultura.Application.DTO;

    public class UserTokenModel : ResponseBase
    {
        public string Email { get; set; }
    }
}