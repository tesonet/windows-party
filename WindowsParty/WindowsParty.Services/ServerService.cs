using System.Collections.Generic;
using WindowsParty.IRepositories;
using WindowsParty.IServices;
using WindowsParty.Models;

namespace WindowsParty.Services
{
    public class ServerService : IServerService
    {
        private readonly IServerRepository _serverRepository;

        public ServerService(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }
        public IEnumerable<Server> GetServers(string accessToken)
        {
            return _serverRepository.GetServers(accessToken);
        }
    }
}