using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tesonet.Client.Services.NavigationService.NavigationData;
using Tesonet.Client.ViewModels;

namespace Tesonet.Client.Tests.ViewModelsTests
{
    [TestClass]
    public class ErrorPageViewModelTests : BaseTests
    {
        [TestMethod]
        public async Task ShouldSetViewModelData_WhenInitializeAsync()
        {
            //arrange
            var errorViewModel = new ErrorPageViewModel(_navigationServiceMock.Object);

            //act
            await errorViewModel.InitializeAsync(new ErrorPageNavigationData
            {
                ErrorTitle = "title",
                ErrorMessage = "error",
                NavigatedFromPage = new ServersPageViewModel(_navigationServiceMock.Object, _serversServiceMock.Object, _settingsServiceMock.Object)
            });

            //assert
            Assert.AreEqual("title", errorViewModel.ErrorTitle);
            Assert.AreEqual("error", errorViewModel.ErrorMessage);
            Assert.IsInstanceOfType(errorViewModel.NavigatedFromPage, typeof(ServersPageViewModel));
        }

        [TestMethod]
        public async Task ShouldNavigateToPreviousPage_WhenOkCommand()
        {
            //arrange
            var errorViewModel = new ErrorPageViewModel(_navigationServiceMock.Object);

            var navigateFromPage = new ServersPageViewModel(_navigationServiceMock.Object, _serversServiceMock.Object,
                _settingsServiceMock.Object);

            await errorViewModel.InitializeAsync(new ErrorPageNavigationData
            {
                NavigatedFromPage = navigateFromPage
            });

            //act
            errorViewModel.OkCommand.Execute(null);

            //assert
            _navigationServiceMock.Verify(x=>x.NavigateToPageAsync(navigateFromPage, It.IsAny<NavigationData>()), Times.Once);
        }
    }
}