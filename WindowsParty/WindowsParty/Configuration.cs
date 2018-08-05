using WindowsParty.ApiServices;
using WindowsParty.ViewModels;
using Unity;
using Unity.Lifetime;

namespace WindowsParty
{
    internal static class Configuration
    {
        private static IUnityContainer _container;

        public static IUnityContainer Container
        {
            get
            {
                if (_container == null)
                    _container=  new UnityContainer();

                return _container;
            }
        }

        public static void ConfigureIoC()
        {
            var c = Container;

            c.RegisterType<IPlaygroundService, PlaygroundService>(new ContainerControlledLifetimeManager());

            c.RegisterType<ILoginViewModel, LoginViewModel>();
            c.RegisterType<IServerListViewModel, ServerListViewModel>();
        }
    }
}
