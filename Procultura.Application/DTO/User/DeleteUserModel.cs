namespace Procultura.Application.DTO.User
{
    using Procultura.Application.DTO;

    public class DeleteUserModel: RequestBase
    {
        public string Email { get; set; }
    }
}