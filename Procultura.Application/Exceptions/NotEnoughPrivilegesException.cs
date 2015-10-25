using System;
using ProCultura.CrossCutting.L10N;

namespace Procultura.Application.Exceptions
{
    public class NotEnoughPrivilegesException : Exception
    {
        public NotEnoughPrivilegesException(string message): base (message){}

        public NotEnoughPrivilegesException() : base(LocalizationKeys.message_InsufficientPrivileges) { }
    }
}
