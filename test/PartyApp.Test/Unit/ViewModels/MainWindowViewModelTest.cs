using PartyApp.Test.Framework;
using PartyApp.ViewModels;
using Shouldly;
using Xunit;

namespace PartyApp.Test.Unit.ViewModels
{
	public class MainWindowViewModelTest
	{
		[AutoMoqData]
		[Theory]
		public void NavigationFromLoginHidesLogin(
			MainWindowViewModel sut,
			string token)
		{
			//act
			sut.NavigateFromLogin();

			//verify
			sut.LoginViewVisible.ShouldBeFalse();
		}

		[AutoMoqData]
		[Theory]
		public void NavigationToLoginHidesServers(
			MainWindowViewModel sut)
		{
			//act
			sut.NavigateToLogin();

			//verify
			sut.ServersViewVisible.ShouldBeFalse();
		}
	}
}
