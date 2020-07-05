namespace ServerFinder.Integration
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IServiceClient
    {
        Task<bool> TryLogIn(NetworkCredential credentials, CancellationToken cancellationToken);
        
        Task<IEnumerable<Server>> GetServerList(CancellationToken cancellationToken);

        void LogOut();
    }
}
