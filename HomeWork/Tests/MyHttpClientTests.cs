using System;
using System.Collections.Generic;
using HomeWork;
using NUnit.Framework;

namespace Tests
    {
    [TestFixture]
    public class MyHttpClientTests
        {
        [Test]
        public void Setup_Send_Success ()
            {
            using ( var client = new MyHttpClient ("tesonet", "partyanimal") )
                {
                client.SetToken ();
                List<Server> servers = client.GetServers ().Result;
                }
            }

        [TestCase("bad", "credentials")]
        [TestCase ("", "")]
        public void Setup_SendBadCredentials_Fails (string username, string password)
            {
            using ( var client = new MyHttpClient (username, password) )
                {
                var ex = Assert.Throws<ArgumentException> (() => client.SetToken ());
                Assert.AreEqual ("Bad credentials.", ex.Message);
                }
            }
        }
    }
