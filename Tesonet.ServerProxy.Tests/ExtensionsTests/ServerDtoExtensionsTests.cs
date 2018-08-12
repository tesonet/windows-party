using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tesonet.ServerProxy.Dto;
using Tesonet.ServerProxy.Exceptions;
using Tesonet.ServerProxy.Extensions;

namespace Tesonet.ServerProxy.Tests.ExtensionsTests
{
    [TestClass]
    public class ServerDtoExtensionsTests
    {
        private static ServerDto _failedDto = new ServerDto();
        private static ServerDto _validDto = new ServerDto();

        [TestInitialize]
        public void Initialize()
        {
            _failedDto = new ServerDto
            {
                Name = "name",
                Distance = "102das"
            };

            _validDto = new ServerDto
            {
                Name = "Ukraine#12",
                Distance = "150"
            };
        }

        [TestMethod]
        [ExpectedException(typeof(RetrieveServerCountryNameException))]
        public void ShouldThrowRetrieveServerCountryNameException_WhenServerDtoNameIsInvalid()
        {
            //assert
            _failedDto.ServerCountryName();
        }

        [TestMethod]
        [ExpectedException(typeof(RetrieveServerNumberException))]
        public void ShouldThrowRetrieveServerNumberException_WhenServerDtoNameIsInvalid()
        {
            //assert
            _failedDto.ServerNumber();
        }

        [TestMethod]
        [ExpectedException(typeof(RetrieveServerNumberException))]
        public void ShouldThrowRetrieveServerNumberException_WhenServerDtoNameIsInvalidAndConvertationFailed()
        {
            //arrange
            _failedDto.Name = "Ukraine#12wa";

            //assert
            _failedDto.ServerNumber();
        }

        [TestMethod]
        [ExpectedException(typeof(RetrieveServerDistanceException))]
        public void ShouldThrowRetrieveServerDistanceException_WhenServerDtoDistanceIsInvalid()
        {
            //assert
            _failedDto.ServerDistance();
        }

        [TestMethod]
        public void ShouldGetServerCountryName_WhenServerDtoNameIsValid()
        {
            //assert
            Assert.AreEqual("Ukraine", _validDto.ServerCountryName());
        }

        [TestMethod]
        public void ShouldGetServerNumber_WhenServerDtoNameIsValid()
        {
            //assert
            Assert.AreEqual(12, _validDto.ServerNumber());
        }

        [TestMethod]
        public void ShouldGetServerDistance_WhenServerDtoDistanceIsValid()
        {
            //assert
            Assert.AreEqual(150, _validDto.ServerDistance());
        }
    }
}