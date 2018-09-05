using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace LoginModule
{
    [ModuleExport(typeof(LoginModule))]
    public class LoginModule : IModule
    {
        [Import]
        public IRegionManager Region { get; set; }

        public void Initialize()
        {
            Region.RequestNavigate("MainRegion", "LoginView");
        }
    }
}
