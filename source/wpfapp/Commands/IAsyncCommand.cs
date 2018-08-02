using System.Threading.Tasks;
using System.Windows.Input;

namespace tesonet.windowsparty.wpfapp.Commands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
