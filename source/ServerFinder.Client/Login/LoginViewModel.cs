namespace ServerFinder.Client.Login
{
    using System.Net;
    using System.Security;
    using System.Threading;
    using System.Threading.Tasks;
    using Caliburn.Micro;
    using ApplicationShell;
    using Integration;

    public class LoginViewModel : Screen
    {
        private readonly IServiceClient _serviceClient;
        private readonly IEventAggregator _eventAggregator;
        
        public LoginViewModel(IServiceClient serviceClient, IEventAggregator eventAggregator)
        {
            _serviceClient = serviceClient;
            _eventAggregator = eventAggregator;
        }

        public async Task LogIn(string username, SecureString password, CancellationToken cancellationToken)
        {
            if (username.Length > 0 && password.Length > 0)
            {
                var credentials = new NetworkCredential(username, password);

                var loggedIn = await _serviceClient.TryLogIn(credentials, cancellationToken);
                if (loggedIn)
                {
                    _eventAggregator.PublishOnUIThread(new ApplicationEvent(ApplicationEventType.LogIn));
                }
            }
        }
    }
}
