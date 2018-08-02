using PubSub;
using System;
using System.Threading.Tasks;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.services.Authentication;
using tesonet.windowsparty.services.Servers;

namespace tesonet.windowsparty.services
{
    public class Publisher : IPublisher
    {
        private readonly Hub _hub;
        private readonly IAuthenticationService _authenticationService;
        private readonly IServersService _serversService;

        public Publisher(IAuthenticationService authenticationService, IServersService serversService)
        {
            _hub = new Hub();
            _authenticationService = authenticationService;
            _serversService = serversService;
        }

        public async Task InitiateAuthentication(Credentials credentials)
        {
            try
            {
                var result = await _authenticationService.Authenticate(credentials);

                _hub.Publish(result);
            }
            catch (AuthenticationServiceException e)
            {
                _hub.Publish(e);
            }
        }

        public async Task InitiateGettingServers(string token)
        {
            try
            {
                var result = await _serversService.Get(token);

                _hub.Publish(result);
            }
            catch (ServersServiceException e)
            {
                _hub.Publish(e);
            }
        }

        public void SubscribeToServers<TSubscriber>(TSubscriber subscriber, Action<Server[]> handler)
        {
            _hub.Subscribe(subscriber, handler);
        }

        public void SubscribeToToken<TSubscriber>(TSubscriber subscriber, Action<string> handler)
        {
            _hub.Subscribe(subscriber, handler);
        }

        public void SubscribeToServers<TSubscriber>(TSubscriber subscriber, Action<ServersServiceException> handler)
        {
            _hub.Subscribe(subscriber, handler);
        }

        public void SubscribeToToken<TSubscriber>(TSubscriber subscriber, Action<AuthenticationServiceException> handler)
        {
            _hub.Subscribe(subscriber, handler);
        }

        public void UnsubscribeFromServers<TSubscriber>(TSubscriber subscriber)
        {
            _hub.Unsubscribe(subscriber);
        }

        public void UnsubscribeFromToken<TSubscriber>(TSubscriber subscriber)
        {
            _hub.Unsubscribe(subscriber);
        }
    }
}
