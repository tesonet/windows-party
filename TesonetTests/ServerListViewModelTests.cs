using Tesonet.ViewModels;
using Tesonet.Services;
using NUnit.Framework;
using FakeItEasy;
using Caliburn.Micro;
using Tesonet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TesonetTests
{
    [TestFixture]
    public class ServerListViewModelTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task LogoutCall_Test()
        {
            IHttpService httpService = A.Fake<IHttpService>();
            IEventAggregator evenAggregator = A.Fake<IEventAggregator>();
            httpService = A.Fake<IHttpService>();
            ServerListViewModel serverListViewModel = new ServerListViewModel(evenAggregator, httpService);

            await serverListViewModel.Servers();

            A.CallTo(() =>  httpService.GetAsync<List<Server>>(A<string>._, A<string>._)).MustHaveHappened();
        }
    }
}
