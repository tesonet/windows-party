using System;
using System.Threading.Tasks;
using RestSharp;

namespace TesonetWpfApp.Business
{
    public interface IRestService
    {
        Task<T> Execute<T>(Uri baseUri, IRestRequest request);
    }
}
