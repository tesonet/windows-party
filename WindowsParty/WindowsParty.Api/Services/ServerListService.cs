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
    public class ServerListService : IServerListService
    {
        private const string _apiUrl = "http://playground.tesonet.lt/v1/";
        private const string _apiOperationToken = "servers";
        private readonly IWindowsLogger _logger;
        private readonly ITokenService _tokenService;

        public ServerListService(IWindowsLogger logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }
        public async Task<List<Server>> GetServerList()
        {
            {
                List<Server> result = null;
                var httpClient = new RestClient(_apiUrl);
                var request = await GetServerListRequest();
                _logger.WriteInformation("Getting Server List");
                try
                {
                    IRestResponse<List<Server>> response = await httpClient.ExecuteTaskAsync<List<Server>>(request);
                    if (response.IsSuccessful) result = response.Data;
                }
                catch (Exception ex)
                {
                    _logger.WriteError("Error calling Server List APi", ex);
                }
                _logger.WriteInformation("Received Server List");
                return result;
            }
        }

        private async Task<RestRequest> GetServerListRequest()
        {
            var tokenDto = await _tokenService.GetToken();
            var result = new RestRequest(_apiOperationToken, Method.GET);
            result.AddParameter("Authorization", string.Format("Bearer {0}", tokenDto.token), ParameterType.HttpHeader);
            result.RequestFormat = DataFormat.Json;
            return result;
        }
    }
}
