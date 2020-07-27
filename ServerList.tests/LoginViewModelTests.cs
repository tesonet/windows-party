using Caliburn.Micro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServerList.Interfaces;
using ServerList.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ServerList.tests
{
    [TestClass]
    public class LoginViewModelTests
    {
        private LoginViewModel _loginViewModel;
        private Mock<IAuthenticationService> _authenticationService;
        private Mock<IServersService> _serversService;
        private Mock<ILog> _logger;
        private PasswordBox _passwordBox;
        private readonly string _token = "s75urj392jfs432sdr";
        private readonly string _password = "password";

        [TestInitialize]
        public void Setup()
        {
            Mock<IEventAggregator> eventAggregator = new Mock<IEventAggregator>();
            _logger = new Mock<ILog>();
            _passwordBox = new PasswordBox
            {
                Password = _password
            };

            HttpResponseMessage loginResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent($"{{ \"token\": \"{_token}\" }}"),
            };

            HttpResponseMessage serversResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{ ""name"": ""Luxemburg"", ""distance"": 157 }, { ""name"": ""Germany"", ""distance"": 200 }]"),
            };

            _authenticationService = new Mock<IAuthenticationService>();
            _authenticationService.Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(loginResponse);

            _serversService = new Mock<IServersService>();
            _serversService.Setup(x => x.GetServersListAsync(It.IsAny<string>())).ReturnsAsync(serversResponse);

            _loginViewModel = new LoginViewModel(eventAggregator.Object, _authenticationService.Object, _serversService.Object, _logger.Object);
        }

        [TestMethod]
        public async Task ShouldCallLoginAsyncWithCredentials()
        {
            string username = "username";
            string password = "password";
            PasswordBox passwordBox = new PasswordBox
            {
                Password = password
            };
            _loginViewModel.Username = username;

            await _loginViewModel.Login(passwordBox);

            _authenticationService.Verify(x => x.LoginAsync(username, password));
        }

        [TestMethod]
        public async Task ShouldCallGetServersListAsyncWithToken()
        {
            await _loginViewModel.Login(_passwordBox);

            _serversService.Verify(x => x.GetServersListAsync(_token));
        }

        [DataTestMethod]
        [DataRow(HttpStatusCode.BadRequest)]
        [DataRow(HttpStatusCode.NotFound)]
        [DataRow(HttpStatusCode.InternalServerError)]
        [DataRow(HttpStatusCode.ServiceUnavailable)]
        public async Task ShouldLogErrorIfLoginAsyncReturnsNotSuccessCode(HttpStatusCode httpStatusCode)
        {
            HttpResponseMessage loginResponse = new HttpResponseMessage
            {
                StatusCode = httpStatusCode,
                Content = new StringContent($"{{ \"token\": \"{_token}\" }}"),
            };
            _authenticationService.Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(loginResponse);

            await _loginViewModel.Login(_passwordBox);

            _logger.Verify(x => x.Error(It.IsAny<Exception>()));
        }

        [DataTestMethod]
        [DataRow(HttpStatusCode.BadRequest)]
        [DataRow(HttpStatusCode.NotFound)]
        [DataRow(HttpStatusCode.InternalServerError)]
        [DataRow(HttpStatusCode.ServiceUnavailable)]
        public async Task ShouldLogErrorIfGetServerListReturnsNotSuccessCode(HttpStatusCode httpStatusCode)
        {
            HttpResponseMessage serversResponse = new HttpResponseMessage
            {
                StatusCode = httpStatusCode,
                Content = new StringContent(@"[{ ""name"": ""Luxemburg"", ""distance"": 157 }, { ""name"": ""Germany"", ""distance"": 200 }]"),
            };
            _serversService.Setup(x => x.GetServersListAsync(It.IsAny<string>())).ReturnsAsync(serversResponse);

            await _loginViewModel.Login(_passwordBox);

            _logger.Verify(x => x.Error(It.IsAny<Exception>()));
        }
    }
}
