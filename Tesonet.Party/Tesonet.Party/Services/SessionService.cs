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
        void Login(string username, string password);
    }

    public class SessionService : ISessionService
    {
        private readonly IUnityContainer container;

        public SessionService(IUnityContainer container)
        {
            this.container = container;
        }

        public async void Login(string username, string password)
        {
            var agent = container.Resolve<ITesonetServiceAgent>();
            var token = await agent.Login(username, password);
        }
    }
}
