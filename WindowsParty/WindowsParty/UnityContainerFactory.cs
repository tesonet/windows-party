using WindowsParty.IRepositories;
using WindowsParty.IServices;
using WindowsParty.Repositories;
using WindowsParty.Services;
using Caliburn.Micro;
using Unity;

namespace WindowsParty
{
    public static class UnityContainerFactory
    {
        private static UnityContainer _unityContainer;

        public static UnityContainer UnityContainer
        {
            get
            {
                if (_unityContainer == null)
                {
                    _unityContainer = new UnityContainer();
                    RegisterTypes();
                }
                return _unityContainer;
            }
        }

        private static void RegisterTypes()
        {
            UnityContainer

                .RegisterType<IWindowManager, WindowManager>()

                // repositories
                .RegisterType<IAuthorizationRepository, AuthorizationRepository>()
                .RegisterType<IServerRepository, ServerRepository>()
            
                // services
                .RegisterType<IAuthorizationService, AuthorizationService>()
                .RegisterType<IServerService, ServerService>()
            
                .RegisterInstance(log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType))

            ;
        }
    }
}