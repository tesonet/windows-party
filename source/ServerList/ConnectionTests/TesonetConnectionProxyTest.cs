using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLister.Service.Interfaces;
using ServiceLister.Common.Implementation;
using Unity;
using UnityConfig = ServerLister.Service.Implementations.UnityConfig;

namespace ServerList.ImplementationTest
{
    [TestClass]
    public class TesonetConnectionProxyTest
    {
        [TestMethod]
        public void GetToken_TokenNotNull()
        {
            Authorization.Instance.GenerateToken("tesonet", "partyanimal");
            Assert.IsNotNull(Authorization.Instance.Token);
        }

        [TestMethod]
        public void GetToken_TokenNull()
        {
            Authorization.Instance.GenerateToken("tesonet", "partyanimal222");
            Assert.IsNull(Authorization.Instance.Token);
        }

        [TestMethod]
        public void GetServerList_ServerListContainsData()
        {
            Authorization.Instance.GenerateToken("tesonet", "partyanimal");
            Assert.IsNotNull(Authorization.Instance.Token);

            UnityConfig.RegisterContainers();
            var serverListService = ServiceLister.Common.Implementation.UnityConfig.Instance.Container
                .Resolve<IServerListerService>();
            var serverList = serverListService.GetServers();
            Assert.IsTrue(serverList != null && serverList.Any());
        }
    }
}