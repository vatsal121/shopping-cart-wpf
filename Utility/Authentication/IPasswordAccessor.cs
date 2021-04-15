using System;

namespace Utility.Authentication
{
    public interface IPasswordAccessor
    {
        event Action PasswordChanged;
        string Password { get; }
        void ClearPassword();
    }
}
