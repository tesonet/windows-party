using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace TesonetWpfApp.Business
{
    public class RestService : IRestService
    {
        private readonly IRestClient _restClient;

        public RestService(IRestClient client)
        {
            _restClient = client;
        }

        public async Task<T> Execute<T>(Uri baseUri, IRestRequest request)
        {
            _restClient.BaseUrl = baseUri;
            var response = await _restClient.ExecuteTaskAsync<T>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            throw new RestException(response.StatusCode, $"Error occured while trying request data, base url: {baseUri}, resource: {request.Resource}");
        }
    }
}
