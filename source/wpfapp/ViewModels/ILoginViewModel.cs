using System.Windows.Input;

namespace tesonet.windowsparty.wpfapp.ViewModels
{
    public interface ILoginViewModel : IViewModel
    {
        string Username { get; set; }

        ICommand LoginCommand { get; }
    }
}
