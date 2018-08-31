using System.Collections.Generic;
using log4net;
using RestSharp;
using ServerLister.Service.Interfaces;
using ServerLister.Service.Interfaces.Dto;
using ServiceLister.Common.Interfaces;

namespace ServerLister.Service.Implementations
{
    public class ServerListerService : IServerListerService
    {
        private readonly ILog _logger;
        private readonly ITesonetConnectionProxy _tesonetConnectionProxy;

        public ServerListerService(ITesonetConnectionProxy tesonetConnectionProxy, ILog logger)
        {
            _tesonetConnectionProxy = tesonetConnectionProxy;
            _logger = logger;
        }

        public List<ServerDto> GetServers()
        {
            var request = new RestRequest(Method.GET) {Resource = "servers"};
            var serverList = _tesonetConnectionProxy.Execute<List<ServerDto>>(request);
            _logger.Debug($"Returned server count: {serverList.Count}");
            return serverList;
        }
    }
}