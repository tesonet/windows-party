using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Tesonet.Party.Services
{
    public interface ISessionService
    {
        Task<string> Login(string username, string password);
        void Logout();
        string Token { get; }
    }

    public class SessionService : ISessionService
    {
        private readonly IUnityContainer container;

        public SessionService(IUnityContainer container)
        {
            this.container = container;
        }

        public async Task<string> Login(string username, string password)
        {
            var agent = container.Resolve<ITesonetServiceAgent>();
            var result = await agent.Login(username, password);
            if (string.IsNullOrEmpty(result.Message))
            {
                Token = result.Token;
                container.Resolve<IShellService>().LoginComplete();
            }

            return result.Message;
        }

        public void Logout()
        {
            Token = null;
            container.Resolve<IShellService>().ShowLogin();
        }

        public string Token { get; private set; }
    }
}
