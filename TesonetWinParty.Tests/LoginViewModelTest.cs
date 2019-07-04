using Caliburn.Micro;
using Moq;
using NUnit.Framework;
using TesonetWinParty.Helpers;
using TesonetWinParty.ViewModels;

namespace TesonetWinParty.Tests
{
    [TestFixture]
    public class LoginViewModelTest
    {
        LoginViewModel _loginVM;

        Mock<IAccountHelper> _accountHelper;
        Mock<IEventAggregator> _eventAggregator;

        [SetUp]
        public void Setup()
        {
            // Mock the window manager
            _accountHelper = new Mock<IAccountHelper>();
            // Mock the event aggregator
            _eventAggregator = new Mock<IEventAggregator>();

            _loginVM = new LoginViewModel(_accountHelper.Object, _eventAggregator.Object);
        }



        [Test]
        public void Test_CanLoginIsTrueWhenUsernameAndPasswordFilled()
        {
            _loginVM.Password = "pasw";
            _loginVM.UserName = "user";
            Assert.True(_loginVM.CanLogIn);
        }

        [Test]
        public void Test_CanLoginIsFalseWhenOneOfInputsIsEmpty()
        {
            _loginVM.Password = "";
            _loginVM.UserName = "user";
            Assert.False(_loginVM.CanLogIn);
            _loginVM.Password = "pasw";
            _loginVM.UserName = "";
            Assert.False(_loginVM.CanLogIn);
        }
    }
}

