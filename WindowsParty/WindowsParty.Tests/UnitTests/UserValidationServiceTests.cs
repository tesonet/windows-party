using AutoFixture;
using AutoFixture.AutoMoq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windowsparty.Model;
using WindowsParty.UI.Services;

namespace WindowsParty.Tests.UnitTests
{
    [TestFixture]
    public class UserValidationServiceTests
    {
        private User _user = new User() { Id = 1, UserName = "Martynas", Password = "Martynas123" };

        [Test]
        [TestCase("Martynas", "Martynas", true)]
        [TestCase("", "M", false)]
        [TestCase("M", "", false)]
        [TestCase("Martynas Samuilovas", null, false)]
        [TestCase("Martynas", "123", true)]
        [TestCase(" ", "Martynas", false)]
        public void CanExecuteLoginTests(string UserName, string password, bool result)
        {
            var sut = new UserValidationService();
            Assert.AreEqual(result, sut.CanExecuteLogin(UserName, password));
        }

        [Test]
        [TestCase("Martynas", "Martynas123", "")]
        [TestCase("artynas", "Martynas123", "Wrong Username")]
        [TestCase("Martynas", "martynas123", "Wrong password")]
        [TestCase("Martynas", "Martynas12", "Wrong password")]
        public void ValidateUserInputTests(string UserName, string password, string result)
        {
            var sut = new UserValidationService();
            Assert.AreEqual(result, sut.ValidateUserInput(_user, UserName, password));
        }
    }
}
