namespace Procultura.Application.Exceptions.Users
{
    using ProCultura.CrossCutting.L10N;

    public class UserNotFoundException : ResourceNotFoundException
    {
        public UserNotFoundException(string message) : base(message) { }

        public UserNotFoundException() : base(LocalizationKeys.message_UserNotFound) { }

    }
}
