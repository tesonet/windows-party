using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windowsparty.Model;
using WindowsParty.Logger.Loggers;

namespace WindowsParty.Api
{
    public class TokenService : ITokenService
    {
        private const string _apiUrl = "http://playground.tesonet.lt/v1/";
        private const string _apiOperationToken = "tokens";
        private readonly IWindowsLogger _logger;

        public TokenService(IWindowsLogger logger)
        {
            _logger = logger;
        }
        public async Task<ApiTokenResponse> GetToken()
        {
            ApiTokenResponse result = null;
            var httpClient = new RestClient(_apiUrl);
            var request = GetTokenRequest();
            _logger.WriteInformation("Getting Token");
            try
            {
                IRestResponse<ApiTokenResponse> response = await httpClient.ExecuteTaskAsync<ApiTokenResponse>(request);
                if (response.IsSuccessful) result = response.Data;
            }
            catch (Exception ex)
            {
                _logger.WriteError("Error calling Token APi", ex);
            }            
            _logger.WriteInformation("Received Token");
            return result;
        }

        private RestRequest GetTokenRequest()
        {
            var result = new RestRequest(_apiOperationToken, Method.POST);
            var bodyDto = new { username = "tesonet", password = "partyanimal" };
            result.AddJsonBody(JsonConvert.SerializeObject(bodyDto));
            result.RequestFormat = DataFormat.Json;
            return result;
        }

    }
}

