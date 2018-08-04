using WindowsParty.ApiServices;
using Unity;
using Unity.Lifetime;

namespace WindowsParty
{
    internal static class Configuration
    {
        private static IUnityContainer _container = _container ?? (_container = new UnityContainer());

        public static IUnityContainer Container => _container;

        public static void ConfigureIoC()
        {
            var c = Container;

            //Singleton
            c.RegisterType<IPlaygroundService, PlaygroundService>(new ContainerControlledLifetimeManager());
        }
    }
}
