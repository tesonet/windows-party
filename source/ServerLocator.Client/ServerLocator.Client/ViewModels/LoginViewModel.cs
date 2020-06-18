using Caliburn.Micro;
using ServerLocator.Client.ServerClient;
using System.Net;
using System.Security;

namespace ServerLocator.Client.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IServerClient serverClient;
        private readonly IEventAggregator eventAggregator;

        public SecureString Password { private get; set; }
        public LoginViewModel(IServerClient serverClient, IEventAggregator eventAggregator)
        {
            this.serverClient = serverClient;
            this.eventAggregator = eventAggregator;
        }

        public async void Login(string username)
        {
            var credentials = new NetworkCredential(username, Password);

            var authenticated = await this.serverClient.TryAuthenticateAsync(credentials);
            if (authenticated)
            {
                eventAggregator.PublishOnUIThread(new ChangeViewMessage(typeof(ServerListViewModel)));
            }

        }
    }
}
