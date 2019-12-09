using Caliburn.Micro;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WindowsParty.Data.Repositories;
using WindowsParty.UI.Services;
using WindowsParty.UI.Views;

namespace WindowsParty.UI.ViewModels
{
    public class LoginViewModel : PropertyChangedBase, INotifyPropertyChanged, ILoginViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Login { get; set; }
        private readonly IUserRepository _repository;
        private readonly IMainViewModel _mainViewModel;
        private readonly IUserValidationService _userValidationService;

        public string UserName { get; set; }
        private string _error;

        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        public LoginViewModel(IUserRepository repository, IMainViewModel mainViewModel, IUserValidationService userValidationService)
        {
            Login = new DelegateCommand<object>(ExecuteLogin, CanExecuteLogin);
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mainViewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
            _userValidationService = userValidationService ?? throw new ArgumentNullException(nameof(userValidationService));
        }

        private bool CanExecuteLogin(object param)
        {
            var password = (PasswordBox)param;
            return _userValidationService.CanExecuteLogin(UserName, password.Password);
        }

        private void ExecuteLogin(object param)
        {
            var password = (PasswordBox)param;
            var user = _repository.GetUser(UserName);
            Error = _userValidationService.ValidateUserInput(user, UserName, password.Password);

            if (Error.Length > 0) return;
            var mainWindow = new MainView();
            mainWindow.DataContext = _mainViewModel;
            mainWindow.Show();
            Application.Current.MainWindow.Close();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
