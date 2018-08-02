using tesonet.windowsparty.services;

namespace tesonet.windowsparty.wpfapp.Factories
{
    public interface IPasswordServiceFactory
    {
        IPasswordService PasswordService { get; }
    }
}
