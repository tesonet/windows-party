using Caliburn.Micro;
using ServerLocator.Client.ServerClient;

namespace ServerLocator.Client.ViewModels
{
    public class ServerListViewModel : Screen
    {
        private readonly IServerClient serverClient;

        public ServerListViewModel(IServerClient serverClient)
        {
            this.serverClient = serverClient;
        }
    }
}
