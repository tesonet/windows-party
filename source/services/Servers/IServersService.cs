using System.Threading.Tasks;
using tesonet.windowsparty.contracts;

namespace tesonet.windowsparty.services.Servers
{
    public interface IServersService
    {
        Task<Server[]> Get(string token);
    }
}
