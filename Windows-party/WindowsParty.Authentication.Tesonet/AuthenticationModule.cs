namespace WindowsParty.Authentication.Tesonet
{
    using Microsoft.Extensions.DependencyInjection;
    using WindowsParty.Authentication.Tesonet.Services;
    using WindowsParty.Domain.Contracts;

    public static class AuthenticationModule
    {
        public static void AddTesonetAuthentication(this IServiceCollection service)
        {
            service.AddHttpClient();
            service.AddTransient<IAuthenticationService, AuthenticationService>();
        }
    }
}