using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testio.Core.Services.ServersService;
using testio.Core.Services.AuthenticationService;
using RestSharp;
using System.Threading;

namespace testio.Services.ServersService
{
    public class ServersService: IServersService
    {
        // pk: app.Config, IConfigService ???
        private const string URL = "http://playground.tesonet.lt/v1/servers";

        #region Constructors

        public ServersService()
        {
        }

        #endregion Constructors

        public async Task<IEnumerable<Server>> GetServerList(string token)
        {
            //await Task.Delay(5000);

            var client = new RestClient(URL);

            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", String.Format("Bearer {0}", token));

            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteTaskAsync<List<Server>>(request, cancellationTokenSource.Token);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            throw new Exception(response.StatusCode.ToString());
        }
    }
}
