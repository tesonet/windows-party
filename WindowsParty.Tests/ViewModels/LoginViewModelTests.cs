using Caliburn.Micro;
using Moq;
using Should;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsParty.Services;
using WindowsParty.ViewModels;
using Xunit;

namespace WindowsParty.Tests.ViewModels
{
	public class LoginViewModelTests
	{
		[Fact]
		public void TestServiceIsCalledOnlyIfValidInput()
		{
			var service = new Mock<IServersService>();
			service
				.Setup(x => x.AuthorizeAsync(It.IsAny<string>(), It.IsAny<string>()))
				.Returns(Task.FromResult(false))
				.Verifiable();

			var vm = new LoginViewModel(Mock.Of<IEventAggregator>(), service.Object);

			vm.Password = string.Empty;
			vm.Username = string.Empty;
			vm.Login();
			service.Verify(x => x.AuthorizeAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

			vm.Username = string.Empty;
			vm.Password = "testpassword";
			vm.Login();
			service.Verify(x => x.AuthorizeAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

			vm.Username = "testusername";
			vm.Password = string.Empty;
			vm.Login();
			service.Verify(x => x.AuthorizeAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

			vm.Username = "testusername";
			vm.Password = "testpassword";
			vm.Login();
			service.Verify(x => x.AuthorizeAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
		}

		[Fact]
		public void TestPropertyChangeNotifications()
		{
			var vm = new LoginViewModel(Mock.Of<IEventAggregator>(), Mock.Of<IServersService>());

			var changedProperties = new List<string>();

			vm.PropertyChanged += (s, e) => changedProperties.Add(e.PropertyName);
			vm.Password = "testpassword";

			changedProperties.Count.ShouldEqual(1);
			changedProperties.ShouldContain(nameof(LoginViewModel.Password));

			vm.Username = "testusername";
			changedProperties.Count.ShouldEqual(2);
			changedProperties.ShouldContain(nameof(LoginViewModel.Username));

			vm.UserError = "error message";
			changedProperties.Count.ShouldEqual(3);
			changedProperties.ShouldContain(nameof(LoginViewModel.UserError));
		}

		[Fact]
		public void TestOnFailedLoginInputsCleared()
		{
			var service = new Mock<IServersService>();
			service
				.Setup(x => x.AuthorizeAsync(It.IsAny<string>(), It.IsAny<string>()))
				.Returns(Task.FromResult(false))
				.Verifiable();

			var vm = new LoginViewModel(Mock.Of<IEventAggregator>(), service.Object);
			vm.Password = "testpasword";
			vm.Username = "testusername";
			vm.UserError = "test error";

			// Authorization shall fail from service
			vm.Login();

			vm.Username.ShouldBeEmpty();
			vm.Password.ShouldBeEmpty();
			vm.UserError.ShouldNotBeEmpty();

			vm.Password = "testpasword";
			vm.Username = string.Empty;

			// Will not pass validation
			vm.Login();

			vm.Username.ShouldBeEmpty();
			vm.Password.ShouldBeEmpty();
			vm.UserError.ShouldNotBeEmpty();
		}

		[Fact]
		public void TestAuthenticationErrorHandled()
		{
			var service = new Mock<IServersService>();
			service
				.Setup(x => x.AuthorizeAsync(It.IsAny<string>(), It.IsAny<string>()))
				.ThrowsAsync(new Exception("message"))
				.Verifiable();

			var vm = new LoginViewModel(Mock.Of<IEventAggregator>(), service.Object)
			{
				Username = "user",
				Password = "pass"
			};
			
			var changedProperties = new List<string>();
			vm.PropertyChanged += (s, e) => changedProperties.Add(e.PropertyName);

			vm.Login();
			service.Verify(x => x.AuthorizeAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

			changedProperties.ShouldContain(nameof(LoginViewModel.UserError));
			vm.UserError.ShouldNotBeEmpty();
		}
	}
}