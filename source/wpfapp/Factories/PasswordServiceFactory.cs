using System;
using tesonet.windowsparty.services;

namespace tesonet.windowsparty.wpfapp.Factories
{
    public class PasswordServiceFactory : IPasswordServiceFactory
    {
        private readonly Func<IPasswordService> _passwordServiceFunc;

        public PasswordServiceFactory(Func<IPasswordService> passwordServiceFunc)
        {
            _passwordServiceFunc = passwordServiceFunc;
        }

        public IPasswordService PasswordService => _passwordServiceFunc();
    }
}
