namespace WindowsParty.Repository.Tesonet
{
    using Microsoft.Extensions.DependencyInjection;
    using WindowsParty.Domain.Contracts;
    using WindowsParty.Repository.Tesonet.Services;

    public static class RepositoryModule
    {
        public static void AddTesonetRepository(this IServiceCollection service)
        {
            service.AddHttpClient();
            service.AddTransient<IServerQueryService, ServersQueryService>();
        }
    }
}