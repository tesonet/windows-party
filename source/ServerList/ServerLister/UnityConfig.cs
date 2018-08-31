using ServerListerApp.Controller;
using ServerListerApp.Interfaces;
using Unity;

namespace ServerListerApp
{
    public static class UnityConfig
    {
        public static void RegisterContainers()
        {
            ServerLister.Service.Implementations.UnityConfig.RegisterContainers();
            var container = ServiceLister.Common.Implementation.UnityConfig.Instance.Container;
            container.RegisterType<IServerListController, ServerListController>();
            container.RegisterType<ILoginController, LoginController>();
            container.RegisterType<IMainFormController, MainFormController>();
        }
    }
}