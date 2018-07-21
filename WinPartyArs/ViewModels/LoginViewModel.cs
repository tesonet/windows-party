using Prism.Commands;
using Prism.Logging;
using Prism.Mvvm;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WinPartyArs.Abstracts;
using WinPartyArs.Common;

namespace WinPartyArs.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private ILoggerFacade _log;
        private TesonetServiceAbstract _tesonetService;

        private CancellationTokenSource _loginCancelationTokenSource;

        #region Properties
        private string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private string errorMsg;
        public string ErrorMsg
        {
            get { return errorMsg; }
            set { SetProperty(ref errorMsg, value); }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand CancelLoginCommand { get; set; }
        #endregion

        public LoginViewModel(ILoggerFacade log, TesonetServiceAbstract tesonetService)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _tesonetService = tesonetService ?? throw new ArgumentNullException(nameof(tesonetService));
            LoginCommand = new DelegateCommand<PasswordBox>(ExecuteLogin, CanExecuteLogin).ObservesProperty(() => Username).ObservesProperty(() => IsBusy);
            CancelLoginCommand = new DelegateCommand(ExecuteCancelLogin).ObservesCanExecute(() => IsBusy);
            _log.Log("LoginViewModel() ctor finished initializing", Category.Debug);
        }

        #region Commands
        private void ExecuteCancelLogin()
        {
            _log.Log("Cancel Login command called", Category.Info);
            lock (this)
            {
                var cs = _loginCancelationTokenSource;
                if (null != cs)
                    cs.Cancel();
            }
        }

        private bool CanExecuteLogin(PasswordBox pb)
        {
            return !string.IsNullOrWhiteSpace(Username) && !IsBusy;
        }

        /// <summary>
        /// This method is designed to returns if previous one is still executing (IsBusy = true), so call cancel command first and let the user
        /// execute this one, only when IsBusy is false. It sets IsBusy to true and tries to log in. On success clears username and password.
        /// </summary>
        private async void ExecuteLogin(PasswordBox pb)
        {
            if (pb == null)
                throw new ArgumentNullException(nameof(pb));

            if (IsBusy)
            {
                _log.Log("ExecuteLogin() called, but IsBusy was true, wait for previous one or cancel it, returning", Category.Warn);
                return;
            }

            IsBusy = true;
            ErrorMsg = null;
            _loginCancelationTokenSource = new CancellationTokenSource();
            try
            {
                _log.Log($"Login command calling DoLoginAsync()", Category.Info);
                var success = await _tesonetService.DoLoginAsync(Username, pb.SecurePassword, _loginCancelationTokenSource.Token);
                _log.Log($"Login command got '{success}' from DoLoginAsync()", Category.Info);
                if (!success)
                {
                    ErrorMsg = "Wrong username or password";
                }
                else
                {
                    ErrorMsg = Username = null;
                    pb.Clear();
                }
            }
            catch (TaskCanceledException)
            {
                _log.Log($"ExecuteLogin() current request was canceled", Category.Debug);
            }
            catch (Exception ex)
            {
                _log.Log($"ExecuteLogin() exception: {ex}", Category.Exception);
                var innerestEx = StaticHelpers.GetInnerMostException(ex);
                if (innerestEx is TesonetServerMessageException)
                    ErrorMsg = $"Login service returned message: {innerestEx.Message}";
                else if (innerestEx is TesonetUnexepectedResponseException)
                    ErrorMsg = $"Login service returned unexpected response: {innerestEx.Message}";
                else
                    ErrorMsg = $"Couldn't reach login service: {innerestEx.Message}";
            }
            finally
            {
                var oldCancelToken = _loginCancelationTokenSource;
                lock (this)
                {
                    _loginCancelationTokenSource = null;
                    oldCancelToken.Dispose();
                }
                IsBusy = false;
            }
        }
        #endregion
    }
}
;