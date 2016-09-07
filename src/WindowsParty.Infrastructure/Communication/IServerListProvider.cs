using System;
using System.Collections.Generic;
using WindowsParty.Infrastructure.Domain;
using RestSharp;

namespace WindowsParty.Infrastructure.Communication
{
    public interface IServerListProvider
    {
        IList<Server> GetServers(string token);
        RestRequestAsyncHandle GetServersAsync(string token, Action<List<Server>> callback);

    }
}