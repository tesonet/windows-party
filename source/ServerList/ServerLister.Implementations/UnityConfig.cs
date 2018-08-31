using ServerLister.Service.Interfaces;
using Unity;

namespace ServerLister.Service.Implementations
{
    public static class UnityConfig
    {
        public static void RegisterContainers()
        {
            var container = ServiceLister.Common.Implementation.UnityConfig.Instance.Container;
            container.RegisterType<IServerListerService, ServerListerService>();
        }
    }
}