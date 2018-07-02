using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WindowsParty.Common.Interfaces;
using WindowsParty.Common.Models;

namespace WindowsParty.Services
{
    public class ServerService : IServerService
    {
        private readonly IAppSettings _appSettings;
        private readonly IUserSessionService _userSessionService;

        public ServerService(IUserSessionService userSessionService, IAppSettings appSettings)
        {
            _userSessionService = userSessionService;
            _appSettings = appSettings;
        }

        public async Task<ServerResultModel> GetServers()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", _userSessionService.GetUser().Token);
                var response = await client.GetAsync(_appSettings.ServersUrl);
                return await ResponseHandler(response);
            }
        }

        private async Task<ServerResultModel> ResponseHandler(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await response.Content.ReadAsStringAsync();
                    var servers = JsonConvert.DeserializeObject<List<ServerResponseModel>>(result);

                    return new ServerResultModel
                    {
                        Success = true,
                        Servers = servers
                    };

                case System.Net.HttpStatusCode.Unauthorized:
                    return new ServerResultModel
                    {
                        Success = false,
                        Message = "Unauthorized"
                    };
                default:
                    return new ServerResultModel
                    {
                        Success = false,
                        Message = "Failed to fetch"
                    };
            }
        }
    }
}
