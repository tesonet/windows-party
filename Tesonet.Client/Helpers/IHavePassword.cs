namespace Tesonet.Client.Helpers
{
    public interface IHavePassword
    {
        System.Security.SecureString Password { get; }
    }
}