namespace Procultura.Application.DTO.User
{
    using DTO;

    public class DeleteUserModel: RequestBase
    {
        public string Email { get; set; }
    }
}