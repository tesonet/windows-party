using System;
using Prism.Regions;

namespace WindowsParty.Infrastructure.Navigation
{
    public class Navigator : INavigator
    {
        private readonly IRegionManager _regionManager;
        private readonly ITitleResolver _titleResolver;

        public Navigator(IRegionManager regionManager, ITitleResolver titleResolver)
        {
            _regionManager = regionManager;
            _titleResolver = titleResolver;
        }

        public void GoTo(string serversView, NavigationParameters parameters = null)
        {
            if (parameters == null)
            {
                _regionManager.RequestNavigate(Regions.MainRegion, new Uri(serversView, UriKind.Relative));
            }
            else
            {
                _regionManager.RequestNavigate(Regions.MainRegion, new Uri(serversView, UriKind.Relative), parameters);
            }

            _titleResolver.ChangeTitle(serversView);
        }
    }
}
