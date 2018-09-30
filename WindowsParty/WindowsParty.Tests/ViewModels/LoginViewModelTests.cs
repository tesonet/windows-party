using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsParty.ViewModels;
using Moq;
using WindowsParty.Services.Contracts;
using log4net;
using Caliburn.Micro;
using System.Threading.Tasks;

namespace WindowsParty.Tests.ViewModels
{
    [TestClass]
    public class LoginViewModelTests
    {
        [DataRow(true)]
        [DataRow(false)]
        [DataTestMethod]
        public async Task Login(bool isLoginSuccess)
        {
            //Setup
            var sessionMock = new Mock<ISessionService>();
            sessionMock.Setup(m => m.Login(It.Is<string>(v => v == "test"), It.Is<string>(v => v == "pass"))).ReturnsAsync(isLoginSuccess);
            var eventAggMock = new Mock<IEventAggregator>();
            var model = new LoginViewModel(sessionMock.Object, eventAggMock.Object);

            //Act
            model.Password = "pass";
            model.Username = "test";
            await model.Login();

            //Assert
            sessionMock.Verify(m => m.Login(It.Is<string>(v => v == "test"), It.Is<string>(v => v == "pass")), Times.Once);
            if (isLoginSuccess)
            {
                Assert.IsNull(model.ErrorMessage);
                Assert.IsNull(model.Username);
                Assert.IsNull(model.Password);
            }
            else
            {
                Assert.IsNotNull(model.ErrorMessage);
                Assert.IsNotNull(model.Username);
                Assert.IsNotNull(model.Password);
            }
        }
    }
}
