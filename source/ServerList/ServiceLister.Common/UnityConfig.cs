using ServiceLister.Common.Interfaces;
using Unity;
using Unity.log4net;

namespace ServiceLister.Common.Implementation
{
    public sealed class UnityConfig
    {
        private static volatile UnityConfig _instance;
        private static readonly object SyncRoot = new object();

        private UnityConfig()
        {
            RegisterContainers();
        }

        public static UnityConfig Instance
        {
            get
            {
                if (_instance == null)
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new UnityConfig();
                    }

                return _instance;
            }
        }

        public UnityContainer Container { get; set; }

        private void RegisterContainers()
        {
            Container = new UnityContainer();
            Container.RegisterType<ITesonetConnectionProxy, TesonetConnectionProxy>();
            Container.AddNewExtension<Log4NetExtension>();
        }
    }
}