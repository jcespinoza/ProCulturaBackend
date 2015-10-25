namespace Procultura.Application.Services.Users
{
    using DTO;
    using DTO.User;

    public interface IUserAppService
    {
        AuthModel GetAuth(LoginModel request);

        ResponseBase DeleteUser(string token, DeleteUserModel request);

        UserModel GetUser(string token);

        UserModel GetUser(string token, int id);

        ResponseBase CreateUser(RegisterModel request);

        ResponseBase UpdateUser(string token, UpdateUserModel request);
    }
}
