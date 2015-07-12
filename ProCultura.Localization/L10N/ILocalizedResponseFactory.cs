namespace ProCultura.Localization.L10N
{
    public interface ILocalizedResponseFactory
    {
        string LoginSuccessMessage();
        string PasswordMismatchMessage();
        string EmailInUseMessage();
        string RegistrationSuccessMessage();
        string AuthRequestNotRecognizedMessage();
        string InsufficientPrivilegesMessage();
        string UserDeletedMessage();
        string UserNotFoundMessage();
        string UpdateUserSuccessMessage();
        string InvalidPasswordMessage();
    }
}