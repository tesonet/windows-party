using Caliburn.Micro;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WindowsParty.Models;
using WindowsParty.Services;
using WindowsParty.ViewModels;
using Xunit;

namespace WindowsParty.Tests.ViewModels
{
	public class ServersViewModelTests
	{
		[Fact]
		public void TestServiceIsCalledOnlyOnceOnActivating()
		{
			var service = new Mock<IServersService>();
			service
				.Setup(x => x.GetServersAsync())
				.Returns(Task.FromResult<ICollection<Server>>(new Collection<Server>()))
				.Verifiable();

			var vm = new ServersViewModel(Mock.Of<IEventAggregator>(), service.Object);

			ScreenExtensions.TryActivate(vm);
			service.Verify(x => x.GetServersAsync(), Times.Once);
		}
	}
}