using System.Threading.Tasks;

namespace Tesonet.ServerProxy.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "");
        Task<TResult> PostAsync<TResult>(string uri, object request, string token = "");
    }
}