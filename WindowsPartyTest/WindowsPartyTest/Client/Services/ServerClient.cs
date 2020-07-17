using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WindowsPartyTest.Client.Base;
using WindowsPartyTest.Client.Services.Interfaces;
using WindowsPartyTest.Models;

namespace WindowsPartyTest.Client.Services
{
    public class ServerClient : WebClientBase, IServerService
    {
        public ServerClient(HttpClient client, APIConfig config) : base(client, config)
        {
        }

        public List<ServerModel> LoadServers()
        {
            try
            {
                var data = Task.Run(() => MakeGetRequest<List<ServerModel>>(_config.ServerEndPoint));
                data.Wait();
                return data.Result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to get servers");
            }
            return new List<ServerModel>();
        }
    }
}
