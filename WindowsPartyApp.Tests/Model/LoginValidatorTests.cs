using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WindowsPartyApp.Model;

namespace WindowsPartyApp.Tests.Model
{
    [TestClass]
    public class LoginValidatorTests
    {
        private LoginValidator loginValidator = new LoginValidator();

        [TestMethod]
        public void Test_If_Validate_UserName_And_Password_Works()
        {
            var credentials = new Credentials { UserName = "", Password = "" };

            Assert.ThrowsException<Exception>(() => loginValidator.ValidateUserNamePassword(credentials), "User Name or Password can not be empty");
        }

        [TestMethod]
        public void Test_If_Validate_Response_Works()
        {
            var authToken = new AuthToken { Token = "" };

            Assert.ThrowsException<Exception>(() => loginValidator.ValidateResponse(authToken), "Incorrect Username or Password");
        }
    }
}
