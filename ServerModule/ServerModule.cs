using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace ServerModule
{
    [ModuleExport(typeof(ServerModule))]
    public class ServerModule : IModule
    {
        [Import]
        public IRegionManager Region { get; set; }

        public void Initialize()
        {
        }
    }
}
