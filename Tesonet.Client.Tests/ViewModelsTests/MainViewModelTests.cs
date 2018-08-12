using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tesonet.Client.ViewModels;
using Tesonet.Client.ViewModels.Base;

namespace Tesonet.Client.Tests.ViewModelsTests
{
    [TestClass]
    public class MainViewModelTests : BaseTests
    {
        [TestMethod]
        public void ShouldSetLoginViewmodelAsMainPage_WhenInitialized()
        {
            //arrange
            var mainViewModel = new MainViewModel(_navigationServiceMock.Object, _authorizationServiceMock.Object, _settingsServiceMock.Object);
            
            //assert
            Assert.IsInstanceOfType(mainViewModel.MainPage, typeof(LoginViewModel));
        }
    }
}