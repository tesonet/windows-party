using Caliburn.Micro;
using Newtonsoft.Json;
using ServerList.Interfaces;
using ServerList.Messages;
using ServerList.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ServerList.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _username;
        private string _exception;

        private readonly IEventAggregator _eventAggregator;
        private readonly IAuthenticationService _authService;
        private readonly IServersService _serversService;
        private readonly ILog _logger;

        public LoginViewModel(IEventAggregator eventAggregator, IAuthenticationService authService, IServersService serversService, ILog logger)
        {
            _eventAggregator = eventAggregator;
            _authService = authService;
            _serversService = serversService;
            _logger = logger;
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Exception
        {
            get { return _exception; }
            set
            {
                _exception = value;
                NotifyOfPropertyChange();
            }
        }

        public async Task Login(PasswordBox passwordBox)
        {
            try
            {
                string token = await GetTokenAsync(_username, passwordBox.Password);
                List<Server> serverList = await GetServersListAsync(token);

                _eventAggregator.PublishOnUIThread(new NavigationMessage(PageName.ServerList));
                _eventAggregator.PublishOnUIThread(new ServerListMessage(serverList));
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Exception = e.Message;
            }
        }

        private async Task<string> GetTokenAsync(string username, string password)
        {
            HttpResponseMessage loginResponse = await _authService.LoginAsync(username, password);

            if (!loginResponse.IsSuccessStatusCode)
            {
                throw new Exception(loginResponse.ReasonPhrase);
            }

            _logger.Info("Successfully logged in!");

            return JsonConvert.DeserializeObject<Token>(await loginResponse.Content.ReadAsStringAsync()).token;
        }

        private async Task<List<Server>> GetServersListAsync(string token)
        {
            HttpResponseMessage serversListResponse = await _serversService.GetServersListAsync(token);

            if (!serversListResponse.IsSuccessStatusCode)
            {
                throw new Exception(serversListResponse.ReasonPhrase);
            }

            _logger.Info("Received server list!");

            return JsonConvert.DeserializeObject<List<Server>>(await serversListResponse.Content.ReadAsStringAsync());
        }
    }

}
