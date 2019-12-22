using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using WindowsParty.Interfaces;
using Caliburn.Micro;
using WindowsParty.ViewModels;
using WindowsParty.Models;
using AutoFixture;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace WindowsParty.Tests
{
    [TestClass]
    public class LoginViewTests
    {
        private Mock<IAuthenticationHelper> _authenticationHelper;
        private Mock<IEventAggregator> _eventAggregator;
        private AuthModel _authModel;
        private LoginViewModel _loginViewModel;

        [TestInitialize]
        public void Setup()
        {
            _authenticationHelper = new Mock<IAuthenticationHelper>();
            _eventAggregator = new Mock<IEventAggregator>();

            _authModel = new AuthModel()
            {
                AuthToken = "f9731b590611a5a9377fbd02f247fcdf"
            };

            _authenticationHelper.Setup(x => x.AuthenticateUser(It.IsAny<UserModel>())).ReturnsAsync(_authModel);

            _loginViewModel = new LoginViewModel(_eventAggregator.Object, _authenticationHelper.Object);
        }

        [TestMethod]
        public async Task TokenIsMapped()
        {
            var fixture = new Fixture();

            UserModel _userModel = fixture.Create<UserModel>();

            _loginViewModel.Username = _userModel.Username;
            _loginViewModel.Password = _userModel.Password;

            await _loginViewModel.AuthUser();

            Assert.AreEqual(_authModel, _loginViewModel.AuthModel);
        }

        [TestMethod]
        public void InvalidInputLength_Username()
        {
            var fixture = new Fixture();

            UserModel _userModel = fixture.Create<UserModel>();

            _loginViewModel.Username = "";
            _loginViewModel.Password = _userModel.Password;

            Assert.AreEqual(false, _loginViewModel.validateInput());
        }

        [TestMethod]
        public void ValidInputLength_Username()
        {
            var fixture = new Fixture();

            UserModel _userModel = fixture.Create<UserModel>();

            _loginViewModel.Username = _userModel.Username;
            _loginViewModel.Password = _userModel.Password;

            Assert.AreEqual(true, _loginViewModel.validateInput());
        }
    }
}
