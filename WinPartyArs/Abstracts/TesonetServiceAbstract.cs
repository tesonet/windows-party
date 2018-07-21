using Prism.Events;
using Prism.Logging;
using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using WinPartyArs.Common;

namespace WinPartyArs.Abstracts
{
    /// <summary>
    /// We can refactor this class to another project, if needed. This can be implemented to have a different way of checking credentials
    /// and getting token and then getting servers with this token. This class has some of the functionality already: it saves _token
    /// and publishes LoginStatusChangedEvent('true' on login and 'false' on logout). To implement this, just implement abstract methods:
    /// LoginAsync that returns token (string), clear your local info on Logout() and return server list on GetServerList().
    /// </summary>
    public abstract class TesonetServiceAbstract
    {
        protected ILoggerFacade _log;
        protected IEventAggregator _eventAggregator;
        protected string _token;

        public TesonetServiceAbstract(ILoggerFacade log, IEventAggregator eventAggregator)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _log.Log("TesonetServiceAbstract() ctor finished initializing", Category.Debug);
        }

        public async Task<bool> DoLoginAsync(string username, SecureString password, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("message", nameof(username));

            _log.Log("DoLoginAsync() calling abstract LoginAsync()", Category.Debug);
            var token = await LoginAsync(username, password, cancellationToken).ConfigureAwait(false);
            if (null == token)
            {
                _log.Log("DoLoginAsync() got NULL from LoginAsync()", Category.Debug);
                return false;
            }
            else
            {
                _log.Log("DoLoginAsync() got token from LoginAsync(), so publishing LoginStatusChangedEvent(true) event", Category.Debug);
                _token = token;
                _eventAggregator.GetEvent<LoginStatusChangedEvent>().Publish(true);
                return true;
            }
        }

        protected abstract Task<string> LoginAsync(string username, SecureString password, CancellationToken cancellationToken);

        public void DoLogout()
        {
            _log.Log("DoLogout() nulling token and calling abstract Logout() and then publishing LoginStatusChangedEvent(false) event", Category.Debug);
            _token = null;
            Logout();
            _eventAggregator.GetEvent<LoginStatusChangedEvent>().Publish(false);
        }

        protected abstract void Logout();

        public abstract Task<dynamic> GetServerList(CancellationToken cancellationToken);
    }
}
