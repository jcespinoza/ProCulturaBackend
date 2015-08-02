namespace ProCultura.Web.Api.Models
{
    using Procultura.Application.DTO;

    public class DeleteUserModel: RequestBase
    {
        public string Email { get; set; }
    }
}