using Moq;
using NUnit.Framework;
using Prism.Regions;
using System.Net;
using System.Security;
using TesonetWpfApp.Business;
using TesonetWpfApp.Utils;
using TesonetWpfApp.ViewModels;
using TesonetWpfApp.Views;

namespace TesonetWpfApp.Tests.ViewModels
{
    public class LoginViewModelTests
    {
        private const string FAKE_TOKEN = "TOKEN";
        private const string FAKE_USERNAME = "USER";
        private const string FAKE_PASS = "PASS";
        private const string NAVIGATION_REGION = "ContentRegion";

        [Test]
        public void LoginCommandDisabledWhenUserHasNotEnteredAnyLoginDetails()
        {
            LoginViewModel vm = new LoginViewModel(null, null);

            Assert.True(string.IsNullOrEmpty(vm.UserName));
            Assert.True(string.IsNullOrEmpty(vm.Password));

            Assert.False(vm.LoginCommand.CanExecute(null));
        }

        [Test]
        public void LoginCommandEnabledWhenUserHasEnteredLoginDetails()
        {
            LoginViewModel vm = GetLoginViewModel(null, null);
            Assert.False(string.IsNullOrEmpty(vm.UserName));
            Assert.False(string.IsNullOrEmpty(vm.Password));
            Assert.True(vm.LoginCommand.CanExecute(null));
        }

        [Test]
        public void LoginFormIsClearedOnNavigation()
        {
            var vm = GetLoginViewModel(null, null);
            Assert.False(string.IsNullOrEmpty(vm.UserName));
            Assert.False(string.IsNullOrEmpty(vm.Password));

            vm.OnNavigatedTo(null);

            Assert.True(string.IsNullOrEmpty(vm.UserName));
            Assert.True(string.IsNullOrEmpty(vm.Password));
        }

        [Test]
        public void UserIsNavigatedToServerListPageOnSuccessfulLogin()
        {
            var regionManager = new Mock<IRegionManager>();
            regionManager.Setup(r => r.RequestNavigate(It.Is<string>(i => i == NAVIGATION_REGION), It.Is<string>(i => i == nameof(ServerList)), It.Is<NavigationParameters>(n => n["token"] as string == FAKE_TOKEN))).Verifiable();

            var service = new Mock<ITesonetService>();
            service.Setup(t => t.GetToken(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(FAKE_TOKEN).Verifiable();

            var paramWrapper = GetMockWrappedSecureString();

            LoginViewModel vm = GetLoginViewModel(regionManager, service);

            vm.LoginCommand.Execute(paramWrapper.Object);

            regionManager.VerifyAll();
            service.VerifyAll();
        }

        [Test]
        public void ErrorIsShownAfterUnsuccessfulLogin()
        {
            var service = new Mock<ITesonetService>();
            service.Setup(s => s.GetToken(It.IsAny<string>(), It.IsAny<string>())).Throws(new RestException(HttpStatusCode.Unauthorized, "error"));
            var vm = GetLoginViewModel(null, service);

            Assert.True(string.IsNullOrEmpty(vm.LoginError));
            vm.LoginCommand.Execute(GetMockWrappedSecureString().Object);

            Assert.False(string.IsNullOrEmpty(vm.LoginError));
            Assert.True(string.IsNullOrEmpty(vm.UserName));
            Assert.True(string.IsNullOrEmpty(vm.Password));
        }

        [Test]
        public void UserIsNotNavigatedToServerListAfterUnsuccessfulLogin()
        {
            var service = new Mock<ITesonetService>();
            service.Setup(s => s.GetToken(It.IsAny<string>(), It.IsAny<string>())).Throws(new RestException(HttpStatusCode.Unauthorized, "error"));
            var regionManager = new Mock<IRegionManager>();
            regionManager.Setup(r => r.RequestNavigate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NavigationParameters>())).Verifiable();

            var vm = GetLoginViewModel(regionManager, service);

            vm.LoginCommand.Execute(GetMockWrappedSecureString().Object);
            regionManager.Verify(r => r.RequestNavigate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NavigationParameters>()), Times.Never);
        }

        #region Helpers
        private static LoginViewModel GetLoginViewModel(IMock<IRegionManager> regionManager, IMock<ITesonetService> service)
        {
            var vm = new LoginViewModel(service?.Object, regionManager?.Object)
            {
                UserName = FAKE_USERNAME,
                Password = FAKE_PASS
            };
            return vm;
        }

        private static Mock<IWrappedParameter<SecureString>> GetMockWrappedSecureString()
        {
            var paramWrapper = new Mock<IWrappedParameter<SecureString>>();
            paramWrapper.Setup(w => w.Value).Returns(new SecureString());
            return paramWrapper;
        }

        #endregion
    }
}