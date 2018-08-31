using System;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Validation;
using ServiceLister.Common.Implementation.Annotations;
using ServiceLister.Common.Interfaces;

namespace ServiceLister.Common.Implementation
{
    public sealed class Authorization : INotifyPropertyChanged
    {
        private static volatile Authorization _instance;
        private static readonly object SyncRoot = new object();
        private readonly string _baseUrl;
        private readonly ILog _logger;
        private ConnectionStatus _connectionStatus;

        private Authorization()
        {
            _baseUrl = ConfigurationManager.AppSettings["TesonetBaseUrl"];
            ConnectionStatus = ConnectionStatus.LoggedOut;
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static Authorization Instance
        {
            get
            {
                if (_instance == null)
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new Authorization();
                    }

                return _instance;
            }
        }

        public ConnectionStatus ConnectionStatus
        {
            get => _connectionStatus;
            set
            {
                _connectionStatus = value;
                OnPropertyChanged();
            }
        }

        public string Token { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void GenerateToken(string userName, string password)
        {
            Require.Argument("User name", userName);
            Require.Argument("Password", password);

            var request = new RestRequest(Method.POST) {Resource = "tokens"};
            request.AddParameter("username", userName);
            request.AddParameter("password", password);

            var client = new RestClient {BaseUrl = new Uri(_baseUrl)};

            var response = client.Execute(request);
            if (response.ErrorException != null)
            {
                if (response.ErrorMessage == "Unauthorized")
                {
                    Token = null;
                    ConnectionStatus = ConnectionStatus.Disconnected;
                    _logger.Debug("User token has expired");
                    return;
                }

                ConnectionStatus = ConnectionStatus.Faulted;
                _logger.Error($"ErrorMessage: {response.ErrorMessage}");
                _logger.Error($"ErrorException: {response.ErrorException}");
                return;
            }
            var responseData = JsonConvert.DeserializeObject<dynamic>(response.Content);
            Token = responseData.token;

            ConnectionStatus = !string.IsNullOrWhiteSpace(Token)
                ? ConnectionStatus.Connected
                : ConnectionStatus.Faulted;

            if (string.IsNullOrWhiteSpace(Token))
                _logger.Debug($"User {userName} failed to login");
        }

        public void DestroyToken()
        {
            Token = null;
            ConnectionStatus = ConnectionStatus.LoggedOut;
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}