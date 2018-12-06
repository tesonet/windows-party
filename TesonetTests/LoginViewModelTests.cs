using Tesonet.ViewModels;
using NUnit.Framework;
using Tesonet.Services;
using FakeItEasy;
using Caliburn.Micro;
using Tesonet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TesonetTests
{
    [TestFixture]
    public class LoginViewModelTests
    {
        [Test]
        public async Task LoginCall_Tests()
        {
            IHttpService httpService = A.Fake<IHttpService>();
            IEventAggregator evenAggregator = A.Fake<IEventAggregator>();
            httpService = A.Fake<IHttpService>();
            LoginViewModel sViewModel = new LoginViewModel(evenAggregator, httpService);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("username", "tesonet");
            headers.Add("password", "partyanimal");
            await sViewModel.Login("test", "test");

            A.CallTo(() =>  httpService.PostDataAsync<LoginResponse>(A<string>._, A<IEnumerable<KeyValuePair<string, string>>>._)).MustHaveHappened();
        }
    }
}
