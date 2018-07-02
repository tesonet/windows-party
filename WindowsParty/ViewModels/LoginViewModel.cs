using System;
using System.Windows.Input;
using Unity;
using WindowsParty.Common.Interfaces;
using WindowsParty.Common.Models;
using WindowsParty.Utils;

namespace WindowsParty.ViewModels
{
    public interface ILoginViewModel {}

    public class LoginViewModel : BaseViewModel, ILoginViewModel
    {
        private string _username;
        private string _passwordInVm;
        private string _errorMessage;
        private bool _isLogging;

        private readonly ILogService _logService;

        public LoginViewModel(IUnityContainer container) : base(container)
        {
            LoginCommand = new RelayCommand(Login);
            _logService = Container.Resolve<ILogService>();
        }

        public bool IsLogging
        {
            get => _isLogging;
            set
            {
                _isLogging = value;
                OnPropertyChanged(nameof(IsLogging));
            }
        }

        public string UserName
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string PasswordInVm
        {
            get => _passwordInVm;
            set
            {
                _passwordInVm = value;
                OnPropertyChanged(nameof(PasswordInVm));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand LoginCommand { get; }

        private async void Login(object parameter)
        {
            try
            {
                ErrorMessage = "";
                IsLogging = true;

                if (parameter is IHavePassword passwordContainer)
                {
                    var secureString = passwordContainer.Password;
                    PasswordInVm = ConvertToUnsecureString(secureString);
                }
                
                var req = new TokenRequestModel
                {
                    Username = this.UserName,
                    Password = this.PasswordInVm
                };

                var result = await Container.Resolve<IAuthorizationService>().GenerateToken(req);

                if (!result.Success)
                {
                    _logService.Info(result.Message);
                    ErrorMessage = result.Message;
                    return;
                }
                
                Container.Resolve<IUserSessionService>().AddUser(new UserSessionModel
                {
                    Username = req.Username,
                    Token = result.Token
                });

                Container.Resolve<IMainViewModel>().ShowServerView();
            }
            catch(Exception ex)
            {
                _logService.Error(ex);
                ErrorMessage = "System error";
            }
            finally
            {
                IsLogging = false;
            }
        }

        /// <summary>
        /// Converts secure pswd to unsecure pswd
        /// </summary>
        /// <param name="securePassword"></param>
        /// <returns></returns>
        private string ConvertToUnsecureString(System.Security.SecureString securePassword)
        {
            if (securePassword == null)
                return string.Empty;

            var unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
