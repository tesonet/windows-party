using Newtonsoft.Json;
using Repository.Model;
using Repository.Repository.Interface;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Repository.Repository
{
    [Export(typeof(IServerRepository))]
    public class ServerRepository : IServerRepository
    {
        public async Task<ServerFarm> GetServerFarmAsync(Authentication authentic)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", authentic.Token);
                return await EstimateResponse(await client.GetAsync(authentic.ServersUrl));
            }
        }

        private async Task<ServerFarm> EstimateResponse(HttpResponseMessage response)
        {
            ServerFarm farm = new ServerFarm();
            switch (farm.Status = response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = await response.Content.ReadAsStringAsync();
                    farm.Servers = JsonConvert.DeserializeObject<ObservableCollection<Server>>(result);
                    return farm;
            }
            return farm;
        }
    }
}
