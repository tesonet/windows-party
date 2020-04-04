using Caliburn.Micro;
using System.Windows.Controls;

namespace WPFApp.ViewModels
{
    public class ShellViewModel: Screen
    {
        private readonly SimpleContainer _container;

        public ShellViewModel(SimpleContainer container)
        {
            _container = container;

        }

        public void RegisterFrame(Frame frame)
        {
            INavigationService _navigationService;
            _navigationService = new FrameAdapter(frame);
            _container.Instance(_navigationService);
            _navigationService.NavigateToViewModel(typeof(LoginViewModel));
        }

    }
}
