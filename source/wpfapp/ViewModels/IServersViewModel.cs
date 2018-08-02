using System.Collections.ObjectModel;
using System.Windows.Input;
using tesonet.windowsparty.contracts;

namespace tesonet.windowsparty.wpfapp.ViewModels
{
    public interface IServersViewModel : IViewModel
    {
        ObservableCollection<Server> Servers { get; }

        ICommand LogoutCommand { get; }
    }
}
