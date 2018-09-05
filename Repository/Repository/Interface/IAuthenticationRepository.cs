using Repository.Model;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface IAuthenticationRepository
    {
        Authentication Authentic { get; }
        Task PostAuthorizationAsync(User user);
    }
}
