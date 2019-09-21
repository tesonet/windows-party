using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WindowsParty.Constants;
using WindowsParty.Handlers;
using WindowsParty.Handlers.Contracts;
using WindowsParty.Models;
using WindowsParty.ViewModels;

namespace WindowsParty.Tests.ViewModels
{
	[TestClass]
	public class ServerListViewModelTests
	{
		#region Test Methods

		[TestMethod]
		public async Task UpdateServers_Success()
		{
			Setup();

			await serversVM.Object.UpdateServers();

			Assert.AreEqual(3, serversVM.Object.Servers.Count);
		}

		#endregion

		#region Properties

		private Mock<ServerListViewModel> serversVM;

		#endregion

		#region Setup

		private void Setup()
		{
			var serversHandlerMock = new Mock<IServersHandler>();
			serversHandlerMock.Setup(lh => lh.GetServers()).Returns(async () =>
			{
				return new List<Server>()
				{
					new Server(){ Name = "Lithuania #1", Distance = 20 },
					new Server(){ Name = "Latvia #2", Distance = 350 },
					new Server(){ Name = "Germany #3", Distance = 1000 }
				};
			});

			var eventAggregatorMock = new Mock<IEventAggregator>();

			serversVM = new Mock<ServerListViewModel>(serversHandlerMock.Object, eventAggregatorMock.Object) { CallBase = true };
		}

		#endregion
	}
}
