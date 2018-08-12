using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Tesonet.Domain.Domain;

namespace Tesonet.ServerProxy.Services.ServersService
{
    public interface IServersService
    {
        Task<ObservableCollection<Server>> GetServersAsync(string url, string token);
    }
}