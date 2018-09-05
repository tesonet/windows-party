using Repository.Model;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface IServerRepository
    {
        Task<ServerFarm> GetServerFarmAsync(Authentication authentic);
    }
}
