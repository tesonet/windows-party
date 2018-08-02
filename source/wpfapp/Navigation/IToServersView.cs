using tesonet.windowsparty.services.Navigation;

namespace tesonet.windowsparty.wpfapp.Navigation
{
    public interface IToServersView : INavigationItem
    {
        string Token { get; }
    }
}
