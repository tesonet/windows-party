using System.Windows.Input;

namespace WindowsParty.ViewModels
{
    public interface IServerListViewModel : IViewModel { }

    public class ServerListViewModel : ViewModel, IServerListViewModel { }
}