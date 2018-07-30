
using Unity;

namespace WindowsParty.ViewModels
{
    public interface IMainViewModel
    {
        void ShowLoginView();
        void ShowServerView();
    }

    /// <summary>
    /// Navigation
    /// </summary>
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        private object _currentViewModel;

        public MainViewModel(IUnityContainer container) : base(container)
        {
            ShowLoginView();
        }

        public void ShowLoginView()
        {
            CurrentViewModel = Container.Resolve<ILoginViewModel>();
        }

        public void ShowServerView()
        {
            CurrentViewModel = Container.Resolve<IServerViewModel>();
        }

        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
    }
}
