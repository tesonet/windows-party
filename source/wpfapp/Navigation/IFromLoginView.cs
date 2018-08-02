using tesonet.windowsparty.services.Navigation;

namespace tesonet.windowsparty.wpfapp.Navigation
{
    public interface IFromLoginView : INavigationItem
    {
        string Token { get; }
    }
}
