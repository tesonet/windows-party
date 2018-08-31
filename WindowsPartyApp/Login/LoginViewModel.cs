using Caliburn.Micro;
using System;
using WindowsPartyApp.Api;
using WindowsPartyApp.Model;
using WindowsPartyApp.Servers;

namespace WindowsPartyApp.Login
{
    public class LoginViewModel: Screen
    {
        private string userName;
        private string password;
        private string message;

        private IApi tesonetApi;
        private readonly ILoginValidator loginValidator;
        private readonly IEventAggregator eventAggregator;

        public LoginViewModel(IApi tesonetApi, IEventAggregator eventAggregator, ILoginValidator loginValidator)
        {
            this.tesonetApi = tesonetApi;
            this.eventAggregator = eventAggregator;
            this.loginValidator = loginValidator;
        }

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public async void Login()
        {
            try
            {
                var credentials = new Credentials { UserName = userName, Password = password };
                loginValidator.ValidateUserNamePassword(credentials);

                var response = await tesonetApi.Login(credentials);
                loginValidator.ValidateResponse(response);

                await eventAggregator.PublishOnUIThreadAsync(new LoginSuccessMessage { ViewModel = new ServersViewModel(response, tesonetApi, eventAggregator) });
                UserName = Password = Message = "";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
    }
}
