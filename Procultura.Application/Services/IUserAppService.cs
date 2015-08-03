namespace Procultura.Application.Services
{
    using Procultura.Application.DTO;
    using Procultura.Application.DTO.User;

    public interface IUserAppService
    {
        AuthModel GetAuth(LoginModel request);

        ResponseBase DeleteUser(string token, DeleteUserModel request);

        UserModel GetUser(string token);

        UserModel GetUser(string token, int id);

        ResponseBase CreateUser(RegisterModel request);

        ResponseBase UpdateUser(string token, UserModel request);
    }
}
