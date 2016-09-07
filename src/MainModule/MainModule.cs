using WindowsParty.Infrastructure;
using Prism.Modularity;
using Prism.Regions;

namespace MainModule
{
    public class MainModule: IModule
    {
        private readonly IRegionManager _regionManager;

        public MainModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(Regions.MainRegion, typeof(LoginView));

            _regionManager.RegisterViewWithRegion(Regions.MainRegion, typeof(ServersView));
        }
    }
}
