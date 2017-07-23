using Prism.Regions;
using System.Windows.Input;
using System.Threading.Tasks;
using TesonetWpfApp.Views;
using Prism.Commands;
using System.Security;
using TesonetWpfApp.Extensions;
using TesonetWpfApp.Business;
using System.Net;
using TesonetWpfApp.Utils;

namespace TesonetWpfApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        private readonly ITesonetService _tesonetService;
        private readonly IRegionManager _regionManager;
        #endregion

        #region Properties
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LoginError { get; private set; }
        #endregion

        #region Commands
        public ICommand LoginCommand { get; }
        #endregion

        public LoginViewModel(ITesonetService tesonetService, IRegionManager regionManager)
        {
            _tesonetService = tesonetService;
            _regionManager = regionManager;

            LoginCommand = new DelegateCommand<IWrappedParameter<SecureString>>(async (password) => await LoginAction(password), (password) => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password)).ObservesProperty(() => UserName).ObservesProperty(() => Password);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            ClearLoginForm();
        }

        private void ClearLoginForm()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }

        private async Task LoginAction(IWrappedParameter<SecureString> password)
        {
            LoginError = string.Empty;

            try
            {
                //Probably not the best way of dealing with SecureStrings but...
                string token = await _tesonetService.GetToken(UserName, password.Value.ToInsecureString());
                _regionManager.RequestNavigate(CONTENT_REGION, nameof(ServerList), new NavigationParameters($"token={token}"));
            }
            catch (RestException e)
            {
                if (e.Code != HttpStatusCode.Unauthorized)
                    throw;

                //bad login
                ClearLoginForm();
                LoginError = "Bad username and/or password entered";
            }
        }
    }
}