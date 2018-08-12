using Moq;
using NUnit.Framework;
using Prism.Regions;
using TesonetWpfApp.Business;
using TesonetWpfApp.Design;
using TesonetWpfApp.ViewModels;

namespace TesonetWpfApp.Tests.ViewModels
{
    public class ServerListViewModelTests
    {
        private const string TOKEN = "TOKEN";

        [Test]
        public void SuccessfulyBindsServerList()
        {
            var tesonetService = new Mock<ITesonetService>();
            tesonetService.Setup(t => t.GetServers(It.IsAny<string>())).ReturnsAsync(new DesignViewModel().Servers); //return the design time collection

            var navContext = new NavigationContext(null, null, new NavigationParameters());
            var vm = new ServerListViewModel(tesonetService.Object, null);
            vm.OnNavigatedTo(navContext); //imitate navigation

            Assert.IsNotNull(vm.Servers);
            Assert.IsNotEmpty(vm.Servers);
        }
    }
}
