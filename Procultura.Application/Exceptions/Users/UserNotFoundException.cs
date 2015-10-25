using ProCultura.CrossCutting.L10N;

namespace Procultura.Application.Exceptions.Users
{
    public class UserNotFoundException : ResourceNotFoundException
    {
        public UserNotFoundException(string message) : base(message) { }

        public UserNotFoundException() : base(LocalizationKeys.message_UserNotFound) { }

    }
}
