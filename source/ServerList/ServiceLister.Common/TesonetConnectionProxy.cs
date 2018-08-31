using System;
using System.Configuration;
using log4net;
using RestSharp;
using ServiceLister.Common.Interfaces;

namespace ServiceLister.Common.Implementation
{
    public class TesonetConnectionProxy : ITesonetConnectionProxy
    {
        private readonly string _baseUrl;
        private readonly ILog _logger;

        public TesonetConnectionProxy(ILog logger)
        {
            _baseUrl = ConfigurationManager.AppSettings["TesonetBaseUrl"];
            _logger = logger;
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient {BaseUrl = new Uri(_baseUrl)};
            var token = Authorization.Instance.Token;

            if (!string.IsNullOrEmpty(token))
                request.AddParameter("Authorization",
                    string.Format("Bearer " + token),
                    ParameterType.HttpHeader);

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                var message = "Error retrieving response.  Check inner details for more info.";
                _logger.Error(message);
                _logger.Error(response.ErrorException);
            }

            if (!response.IsSuccessful)
            {
                var message = response.StatusCode.ToString();
                if (message == "Disconnected")
                    Authorization.Instance.ConnectionStatus = ConnectionStatus.Disconnected;
                _logger.Error(
                    $"Call to {_baseUrl}/{request.Resource} was unsuccessful with status code {response.StatusCode}");
            }
            return response.Data;
        }
    }
}