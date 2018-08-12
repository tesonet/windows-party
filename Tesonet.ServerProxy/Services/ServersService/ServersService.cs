using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tesonet.Domain.Domain;
using Tesonet.ServerProxy.Dto;
using Tesonet.ServerProxy.Mapper;
using Tesonet.ServerProxy.Services.RequestProvider;

namespace Tesonet.ServerProxy.Services.ServersService
{
    public class ServersService : IServersService
    {
        private readonly IRequestProvider _requestProvider;

        public ServersService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<Server>> GetServersAsync(string url, string token)
        {
            var response = await _requestProvider.GetAsync<IEnumerable<ServerDto>>(url, token);
            return new ObservableCollection<Server>(response.Select(x => x.MapToServer()).OrderBy(x => x.Country).ThenBy(x => x.Number));
        }
    }
}