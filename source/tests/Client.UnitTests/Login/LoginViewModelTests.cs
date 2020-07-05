namespace Client.UnitTests.Login
{
    using Caliburn.Micro;
    using NSubstitute;
    using ServerFinder.Client.ApplicationShell;
    using ServerFinder.Client.Login;
    using ServerFinder.Integration;
    using System.Net;
    using System.Security;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class LoginViewModelTests
    {
        private readonly LoginViewModel _sut;
        private readonly IServiceClient _serviceClient = Substitute.For<IServiceClient>();
        private readonly IEventAggregator _eventAggregator = Substitute.For<IEventAggregator>();

        public LoginViewModelTests()
        {
            _sut = new LoginViewModel(_serviceClient, _eventAggregator);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("Username", "")]
        [InlineData("", "Password")]
        public async Task ItDoesNothing_WhenCredentialsNotSupplied(string username, string password)
        {
            // Act
            await _sut.LogIn(username, ToSecureString(password), CancellationToken.None);

            // Assert
            await _serviceClient.DidNotReceive().TryLogIn(Arg.Any<NetworkCredential>(), Arg.Any<CancellationToken>());
            _eventAggregator.DidNotReceive().PublishOnUIThread(Arg.Any<ApplicationEvent>());
        }

        [Fact]
        public async Task ItTriesToLogin_Unsuccessfully()
        {
            // Prepare
            var password = "password";
            var username = "username";
            _serviceClient.TryLogIn(Arg.Any<NetworkCredential>(), Arg.Any<CancellationToken>()).Returns(false);

            // Act
            await _sut.LogIn(username, ToSecureString(password), CancellationToken.None);

            // Assert
            await _serviceClient.Received(1)
                .TryLogIn(Arg.Is<NetworkCredential>(nc => nc.Password == password && nc.UserName == username), Arg.Any<CancellationToken>());
            _eventAggregator.DidNotReceive().PublishOnUIThread(Arg.Any<ApplicationEvent>());
        }

        [Fact]
        public async Task ItTriesToLogin_Successfully()
        {
            // Prepare
            var password = "password";
            var username = "username";
            _serviceClient.TryLogIn(Arg.Any<NetworkCredential>(), Arg.Any<CancellationToken>()).Returns(true);

            // Act
            await _sut.LogIn(username, ToSecureString(password), CancellationToken.None);

            // Assert
            await _serviceClient.Received(1)
                .TryLogIn(Arg.Is<NetworkCredential>(nc => nc.Password == password && nc.UserName == username), Arg.Any<CancellationToken>());
            _eventAggregator.Received(1)
                .PublishOnUIThread(Arg.Is<ApplicationEvent>(av => av.Type == ApplicationEventType.LogIn));
        }

        private static SecureString ToSecureString(string input)
        {
            var secureString = new SecureString();
            foreach (char ch in input)
            {
                secureString.AppendChar(ch);
            }

            return secureString;
        }
    }
}
