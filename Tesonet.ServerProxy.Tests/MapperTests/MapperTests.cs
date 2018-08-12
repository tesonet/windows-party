using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tesonet.ServerProxy.Dto;
using Tesonet.ServerProxy.Mapper;

namespace Tesonet.ServerProxy.Tests.MapperTests
{
    [TestClass]
    public class MapperTests
    {
        [TestMethod]
        public void ShouldMapServerDtoToServer_WhenMapIsCalled()
        {
            //arrange
            var dto = new ServerDto
            {
                Name = "Ukraine#12",
                Distance = "1000"
            };

            //act
            var server = dto.MapToServer();

            //assert
            Assert.AreEqual("Ukraine", server.Country);
            Assert.AreEqual(12, server.Number);
            Assert.AreEqual(1000, server.Distance);
        }
    }
}