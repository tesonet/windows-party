using Prism.Regions;

namespace WindowsParty.Infrastructure.Navigation
{
    public interface INavigator
    {
        void GoTo(string serversView, NavigationParameters parameters = null);
    }
}