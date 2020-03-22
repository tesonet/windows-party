using Caliburn.Micro;
using System.Windows.Controls;

namespace WindowsParty.App.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly SimpleContainer _container;
        private INavigationService _navigationService;

        public ShellViewModel(SimpleContainer container)
        {
            this._container = container;
        }

        public void RegisterFrame(Frame frame)
        {
            _navigationService = new FrameAdapter(frame);
            _container.Instance(_navigationService);
            _navigationService.NavigateToViewModel(typeof(LoginViewModel));
        }
    }
}