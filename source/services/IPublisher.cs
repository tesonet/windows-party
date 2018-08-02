using System;
using System.Threading.Tasks;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.services.Authentication;
using tesonet.windowsparty.services.Servers;

namespace tesonet.windowsparty.services
{
    public interface IPublisher
    {
        Task InitiateAuthentication(Credentials credentials);

        Task InitiateGettingServers(string token);

        void SubscribeToToken<TSubscriber>(TSubscriber sender, Action<string> handler);

        void SubscribeToToken<TSubscriber>(TSubscriber sender, Action<AuthenticationServiceException> handler);

        void SubscribeToServers<TSubscriber>(TSubscriber sender, Action<Server[]> handler);

        void SubscribeToServers<TSubscriber>(TSubscriber sender, Action<ServersServiceException> handler);

        void UnsubscribeFromToken<TSubscriber>(TSubscriber sender);

        void UnsubscribeFromServers<TSubscriber>(TSubscriber sender);
    }
}
