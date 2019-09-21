using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WindowsParty.Constants;
using WindowsParty.Handlers;
using WindowsParty.Handlers.Contracts;
using WindowsParty.ViewModels;

namespace WindowsParty.Tests.ViewModels
{
	[TestClass]
	public class LoginViewModelTests
	{
		#region Test Methods

		[TestMethod]
		public async Task LoginAsync_EmptyUsernameAndPassword_Fail()
		{
			Setup();

			loginVM.Object.Username = "";
			loginVM.Object.Password = "";
			await loginVM.Object.LoginAsync();

			Assert.AreEqual(DefaultValues.LOGIN_FAILED_TITLE, errorTitle);
			Assert.AreEqual($"{DefaultValues.ERROR_USERNAME_BLANK}{Environment.NewLine}{DefaultValues.ERROR_PASSWORD_BLANK}", errorMessage);
		}
		
		[TestMethod]
		public async Task LoginAsync_NotCorrectUsernameAndPassword_Fail()
		{
			Setup();

			loginVM.Object.Username = "not_correct";
			loginVM.Object.Password = "not_correct";
			await loginVM.Object.LoginAsync();

			Assert.AreEqual(DefaultValues.LOGIN_FAILED_TITLE, errorTitle);
			Assert.AreEqual(DefaultValues.LOGIN_FAILED_TEXT, errorMessage);
		}

		[TestMethod]
		public async Task LoginAsync_Success()
		{
			Setup();

			loginVM.Object.Username = "test";
			loginVM.Object.Password = "test";
			await loginVM.Object.LoginAsync();

			Assert.AreEqual(null, errorTitle);
			Assert.AreEqual(null, errorMessage);
		}

		#endregion

		#region Properties

		private Mock<LoginViewModel> loginVM;
		private string errorTitle;
		private string errorMessage;

		#endregion

		#region Setup

		private void Setup()
		{
			var loginHandlerMock = new Mock<ILoginHandler>();
			loginHandlerMock.Setup(lh => lh.Login(It.IsAny<string>(), It.IsAny<string>())).Returns<string, string>(async (username, password) =>
			{
				if (username == "test" && password == "test")
					return true;
				return false;
			});

			var eventAggregatorMock = new Mock<IEventAggregator>();

			loginVM = new Mock<LoginViewModel>(loginHandlerMock.Object, eventAggregatorMock.Object) { CallBase = true };
			loginVM.Setup(lvm => lvm.ShowMessage(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((message, title) =>
			{
				errorTitle = title;
				errorMessage = message;
			});
			loginVM.Setup(lvm => lvm.NavigateToServerListScreen()).Returns(Task.CompletedTask);
		}

		#endregion
	}
}
