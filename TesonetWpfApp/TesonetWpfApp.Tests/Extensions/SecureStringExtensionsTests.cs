using System.Net;
using NUnit.Framework;
using TesonetWpfApp.Extensions;

namespace TesonetWpfApp.Tests.Extensions
{
    public class SecureStringExtensionsTests
    {
        [Test]
        public void ReturnsCorrectString()
        {
           var cred = new NetworkCredential("user", "pass");
           var secure = cred.SecurePassword;

            var insecurePass = secure.ToInsecureString();
            Assert.AreEqual("pass",insecurePass);
        }
    }
}