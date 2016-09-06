using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Language.Flow;
using PartyApp.Models;
using PartyApp.Services;
using PartyApp.Test.Framework;
using PartyApp.ViewModels;
using PartyApp.ViewModelsEvents;
using Ploeh.AutoFixture.Xunit2;
using Shouldly;
using Xunit;

namespace PartyApp.Test.Unit.ViewModels
{
	public class LoginViewModelTest
	{
		[AutoMoqData]
		[Theory]
		public void AuthorizerIsNotCalledWithInvalidCredentials(
			[Frozen]Mock<ILoginModel> loginModel,
			LoginViewModel sut,
			AuthorizationResult result)
		{
			//prepare
			sut.UserName = "";
			sut.Password = null;

			loginModel.Setup().
				ReturnsAsync(result);

			//act
			sut.LoginCommand.Execute(null);

			//verify
			loginModel.Verify(m => m.Login(
				It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<CancellationToken>()), Times.Never());
		}

		[AutoMoqData]
		[Theory]
		public void FailedLoginNotifies(
			[Frozen]Mock<ILoginModel> loginModel,
			LoginViewModel sut,
			string userName,
			string password,
			EventTracker<ErrorEvent, ErrorPayload> eventTracker,
			string errorMessage)
		{
			//prepare
			sut.UserName = userName;
			sut.Password = password;

			loginModel.Setup().
				ReturnsAsync(new AuthorizationResult(errorMessage));

			//act
			sut.LoginCommand.Execute(null);

			//verify
			eventTracker.IsPublished.ShouldBeTrue();
		}

		[AutoMoqData]
		[Theory]
		public void SuccessfulLoginNavigates(
			[Frozen]Mock<ILoginModel> loginModel,
			LoginViewModel sut,
			string userName,
			string password,
			AuthorizationResult authorizationResult,
			EventTracker<NavigationFromLoginRequestedEvent, NavigatiomFromLoginPayload> eventTracker)
		{
			//prepare
			sut.UserName = userName;
			sut.Password = password;

			loginModel.Setup().
				ReturnsAsync(authorizationResult);

			//act
			sut.LoginCommand.Execute(null);

			//verify
			eventTracker.IsPublished.ShouldBeTrue();
		}

		[AutoMoqData]
		[Theory]
		public void PropertyChangeNotificationAreRaised(
			LoginViewModel sut,
			string errorMessage,
			string userName,
			string password)
		{
			//prepare, act
			var errorTracker = new PropertyChangeTracker(sut, nameof(LoginViewModel.ErrorMessage), () =>
			{
				sut.ErrorMessage = errorMessage;
			});

			//verify
			errorTracker.HasChanged.ShouldBeTrue();

			//prepare, act
			errorTracker = new PropertyChangeTracker(sut, nameof(LoginViewModel.UserName), () =>
			{
				sut.UserName = userName;
			});

			//verify
			errorTracker.HasChanged.ShouldBeTrue();

			//prepare, act
			errorTracker = new PropertyChangeTracker(sut, nameof(LoginViewModel.Password), () =>
			{
				sut.Password = password;
			});

			//verify
			errorTracker.HasChanged.ShouldBeTrue();
		}
	}

	public static class SetupExtensions
	{
		public static ISetup<ILoginModel, Task<AuthorizationResult>> Setup(this Mock<ILoginModel> model)
		{
			return model.Setup(p => p.Login(
				It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<CancellationToken>()));
		}
	}
}
