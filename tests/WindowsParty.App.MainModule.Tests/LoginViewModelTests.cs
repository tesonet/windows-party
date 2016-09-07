using System.Net;
using WindowsParty.Infrastructure;
using WindowsParty.Infrastructure.Communication;
using WindowsParty.Infrastructure.Navigation;
using MainModule;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Prism.Regions;

namespace WindowsParty.App.MainModule.Tests
{
    [TestFixture]
    public class LoginViewModelTests
    {
        private Mock<INavigator> _navigatorMock;
        private LoginViewModel _sut;
        private Mock<IAuthenticator> _authenticatorMock;

        [SetUp]
        public void Init()
        {
            _authenticatorMock = new Mock<IAuthenticator>();
            _navigatorMock = new Mock<INavigator>();
            _sut = new LoginViewModel(_navigatorMock.Object, _authenticatorMock.Object);
        }

        [Test, AutoData]
        public void LoginCommand_NavigatesToServersView_IfAuthenticationSuccessfull(string token)
        {
            const HttpStatusCode status = HttpStatusCode.OK;
            _authenticatorMock.Setup(t => t.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(status);
            _authenticatorMock.Setup(t => t.Token).Returns(token);

            _sut.LoginCommand.Execute(new { });

            _navigatorMock.Verify(t => t.GoTo(AppViews.ServersView, It.Is<NavigationParameters>(p => (string)p["token"] == token)), Times.Once);
        }

        [Test]
        public void LoginCommand_SetsErrorOccurredToFalse_IfAuthenticationSuccessfull()
        {
            const HttpStatusCode status = HttpStatusCode.OK;
            _authenticatorMock.Setup(t => t.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(status);

            _sut.LoginCommand.Execute(new { });

            Assert.False(_sut.ErrorOccurred);
        }


        [Test]
        public void LoginCommand_DoesNotNavigateToServersView_IfAuthenticationUnsuccessfull()
        {
            _authenticatorMock.Setup(t => t.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(HttpStatusCode.BadRequest);

            _sut.LoginCommand.Execute(new { });

            _navigatorMock.Verify(t => t.GoTo(AppViews.ServersView, It.IsAny<NavigationParameters>()), Times.Never);
        }


        [Test]
        public void LoginCommand_Authenticates()
        {
            _sut.LoginCommand.Execute(new { });

            _authenticatorMock.Verify(t => t.Authenticate(_sut.Username, _sut.Password), Times.Once);
        }

        [Test]
        public void ErrorOccurred_RaisesPropertyChanged()
        {
            var wasRaised = false;
            _sut.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_sut.ErrorOccurred))
                {
                    wasRaised = true;
                }
            };

            _sut.ErrorOccurred = true;

            Assert.True(wasRaised);
        }

        [Test]
        public void LoginCommand_SetsErrorOccurredToTrue_IfAuthenticationUnsuccessfull()
        {
            _authenticatorMock.Setup(t => t.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(HttpStatusCode.BadRequest);

            _sut.LoginCommand.Execute(new { });

            Assert.True(_sut.ErrorOccurred);
        }

    }
}
