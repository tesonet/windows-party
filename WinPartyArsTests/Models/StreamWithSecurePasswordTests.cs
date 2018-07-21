using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using Prism.Logging;
using Newtonsoft.Json;

namespace WinPartyArs.Models.Tests
{
    [TestClass()]
    public class StreamWithSecurePasswordTests
    {
        private const string JSON_LOGIN_PREFIX = @"{""username"":""tenoset"",""password"":""";
        private const string JSON_LOGIN_SUFFIX = @"""}";

        /// <summary>
        /// For now will test escape charetcers only these (used in TesonetService for JSON password)
        /// </summary>
        private static readonly Dictionary<char, byte[]> jsonEscapedCharsToBytes = new Dictionary<char, byte[]>
        {
            { '\"', new byte[] { (byte)'\\', (byte)'"' } },  // change " to \"
            { '\\', new byte[] { (byte)'\\', (byte)'\\' } }, // change \ to \\
            /* this probably can't be used in passwords, so will ignore all control characters, uncomment if needed
            {  '/', new byte[] { (byte)'\\', (byte)'/' } },  // change / to \/
            { '\b', new byte[] { (byte)'\\', (byte)'b' } },  // change BACKSPACE to \b
            { '\f', new byte[] { (byte)'\\', (byte)'f' } },  // change FORMFEED to \f
            { '\n', new byte[] { (byte)'\\', (byte)'n' } },  // change NEWLINE to \n
            { '\r', new byte[] { (byte)'\\', (byte)'r' } },  // change CarriageReturn to \r
            { '\t', new byte[] { (byte)'\\', (byte)'t' } },  // change TAB to \t
            */
        };

        private static SecureString GetSecureString(string s)
        {
            var res = new SecureString();
            foreach (var c in s)
                res.AppendChar(c);
            return res;
        }

        private static string GetStreamResult(string password, Encoding enc, byte[] buff, int step,
            string prefix = JSON_LOGIN_PREFIX, string suffix = JSON_LOGIN_SUFFIX,
            Dictionary<char, byte[]> escapedCharsToBytes = null)
        {
            var s = new StreamWithSecurePassword(new EmptyLogger(), prefix, GetSecureString(password), suffix, enc,
                null == escapedCharsToBytes && enc == Encoding.UTF8 ? jsonEscapedCharsToBytes : escapedCharsToBytes);
            int i = 0, read;
            do
            {
                read = s.Read(buff, i, step);
                i += read;
            } while (i < buff.Length - step && read == step);
            return enc.GetString(buff, 0, i);
        }

        [TestMethod()]
        public void StreamWithSecurePasswordUTF8Step10Test()
        {
            string password = "animalparty";
            var result = GetStreamResult(password, Encoding.UTF8, new byte[1024], 10);
            Assert.AreEqual(JSON_LOGIN_PREFIX + password + JSON_LOGIN_SUFFIX, result);
        }

        [TestMethod()]
        public void StreamWithSecurePasswordUTF8Step1024Test()
        {
            string password = "animalparty";
            var result = GetStreamResult(password, Encoding.UTF8, new byte[1024], 1024);
            Assert.AreEqual(JSON_LOGIN_PREFIX + password + JSON_LOGIN_SUFFIX, result);
        }

        [TestMethod()]
        public void StreamWithSecurePasswordUTF16Step10Test()
        {
            string password = "animalparty";
            var result = GetStreamResult(password, Encoding.Unicode, new byte[1024], 10);
            Assert.AreEqual(JSON_LOGIN_PREFIX + password + JSON_LOGIN_SUFFIX, result);
        }

        [TestMethod()]
        public void StreamWithSecurePasswordUTF8Step1SuperFatSymbolsTest()
        {
            string password = "ꧮ€ꧮ€ꧮ€ꧮ";
            var result = GetStreamResult(password, Encoding.UTF8, new byte[1024], 1);
            Assert.AreEqual(JSON_LOGIN_PREFIX + password + JSON_LOGIN_SUFFIX, result);
        }

        [TestMethod()]
        public void StreamWithSecurePasswordCtorArgumentNullExceptionsTest()
        {
            string password = "animalparty";
            Encoding enc = Encoding.UTF8;
            Assert.ThrowsException<ArgumentNullException>(() => new StreamWithSecurePassword(null, JSON_LOGIN_PREFIX,
                GetSecureString(password), JSON_LOGIN_SUFFIX, enc, jsonEscapedCharsToBytes), "Argument 'log' passed NULL");
            Assert.ThrowsException<ArgumentNullException>(() => new StreamWithSecurePassword(new EmptyLogger(), null,
                GetSecureString(password), JSON_LOGIN_SUFFIX, enc, jsonEscapedCharsToBytes), "Argument 'prefix' passed NULL");
            Assert.ThrowsException<ArgumentNullException>(() => new StreamWithSecurePassword(new EmptyLogger(), JSON_LOGIN_PREFIX,
                null, JSON_LOGIN_SUFFIX, enc, jsonEscapedCharsToBytes), "Argument 'password' passed NULL");
            Assert.ThrowsException<ArgumentNullException>(() => new StreamWithSecurePassword(new EmptyLogger(), JSON_LOGIN_PREFIX,
                GetSecureString(password), null, enc, jsonEscapedCharsToBytes), "Argument 'suffix' passed NULL");
            Assert.ThrowsException<ArgumentNullException>(() => new StreamWithSecurePassword(new EmptyLogger(), JSON_LOGIN_PREFIX,
                GetSecureString(password), JSON_LOGIN_SUFFIX, null, jsonEscapedCharsToBytes), "Argument 'enc' passed NULL");
        }

        [TestMethod()]
        public void StreamWithSecurePasswordReadArgumentExceptionsTest()
        {
            string password = "animalparty";
            Encoding enc = Encoding.UTF8;
            byte[] buff = new byte[10];
            var s = new StreamWithSecurePassword(new EmptyLogger(), JSON_LOGIN_PREFIX, GetSecureString(password), JSON_LOGIN_SUFFIX,
                enc, jsonEscapedCharsToBytes);
            Assert.ThrowsException<ArgumentNullException>(() => s.Read(null, 0, 1), "Argument 'buff' passed NULL");
            Assert.ThrowsException<ArgumentException>(() => s.Read(buff, -1, 1), "Argument 'offset' passed -1");
            Assert.ThrowsException<ArgumentException>(() => s.Read(buff, 10, 1), "Argument 'offset' passed 10, when buff.Length was 10");
            Assert.ThrowsException<ArgumentException>(() => s.Read(buff, 6, 5), "Argument 'offset' passed 6, when buff.Length was 10 and offset 5");
            Assert.ThrowsException<ArgumentException>(() => s.Read(buff, 0, 0), "Argument 'count' passed 0");
            Assert.ThrowsException<ArgumentException>(() => s.Read(buff, 0, 11), "Argument 'count' passed 11, when buff.Length was 10");
        }

        [TestMethod()]
        public void StreamWithSecurePasswordControlSymbolsExceptionsTest()
        {
            Encoding enc = Encoding.UTF8;
            Assert.ThrowsException<ArgumentException>(() => GetStreamResult("one\rtwo", Encoding.UTF8, new byte[1024], 1024),
                "Password containing '\\r (step 1024)'");
            Assert.ThrowsException<ArgumentException>(() => GetStreamResult("one\ntwo", Encoding.UTF8, new byte[1024], 1),
                "Password containing '\\n (step 1)'");
            Assert.ThrowsException<ArgumentException>(() => GetStreamResult("\bone two", Encoding.UTF8, new byte[1024], 10),
                "Password containing '\\b (step 10)'");
            Assert.ThrowsException<ArgumentException>(() => GetStreamResult("one two\f", Encoding.UTF8, new byte[1024], 10),
                "Password containing '\\f (step 10)'");
            Assert.ThrowsException<ArgumentException>(() => GetStreamResult("one \t\t two", Encoding.UTF8, new byte[1024], 10),
                "Password containing '\\t (step 10)'");
        }

        [TestMethod()]
        public void StreamWithSecurePasswordBadEscapeBytesExceptionsTest()
        {
            string password = "animalparty";
            Encoding enc = Encoding.UTF8;
            Assert.ThrowsException<ArgumentException>(() => new StreamWithSecurePassword(new EmptyLogger(), JSON_LOGIN_PREFIX,
                GetSecureString(password), JSON_LOGIN_SUFFIX, enc, new Dictionary<char, byte[]>() { { 'a', new byte[] { } } }),
                "One char to 0 Length byte array");
            Assert.ThrowsException<ArgumentException>(() => new StreamWithSecurePassword(new EmptyLogger(), JSON_LOGIN_PREFIX,
                GetSecureString(password), JSON_LOGIN_SUFFIX, enc, new Dictionary<char, byte[]>() { { ' ', new byte[] { 32 } }, { 'a', null } }),
                "Two chars, second to NULL byte array");
        }

        [TestMethod()]
        public void StreamWithSecurePasswordUTF8Step10EscapedSymbolsTest()
        {
            string password = "\"\\\"a\\\\n\"\"im\"\"\"\"a{l[<./_-+  p\"'!'\\ar%^*~`'\\\"ty\"";
            var result = GetStreamResult(password, Encoding.UTF8, new byte[1024], 10);
            var expectedByJson = JsonConvert.ToString(password);
            expectedByJson = expectedByJson.Substring(1, expectedByJson.Length - 2);
            Assert.AreEqual(JSON_LOGIN_PREFIX + expectedByJson + JSON_LOGIN_SUFFIX, result);
        }

        [TestMethod()]
        public void StreamWithSecurePasswordUTF8Step1EscapedSymbolsTest()
        {
            string password = "\"\\\"a\\\\n\"\"im\"\"\"\"a{l[<./_-+  p\"'!'\\ar%^*~`'\\\"ty\"";
            var result = GetStreamResult(password, Encoding.UTF8, new byte[1024], 1);
            var expectedByJson = JsonConvert.ToString(password);
            expectedByJson = expectedByJson.Substring(1, expectedByJson.Length - 2);
            Assert.AreEqual(JSON_LOGIN_PREFIX + expectedByJson + JSON_LOGIN_SUFFIX, result);
        }

        [TestMethod()]
        public void StreamWithSecurePasswordUTF8Step10EmptyPrefixTest()
        {
            string password = "animalparty", prefix = "";
            var result = GetStreamResult(password, Encoding.UTF8, new byte[1024], 10, prefix: prefix);
            Assert.AreEqual(prefix + password + JSON_LOGIN_SUFFIX, result);
        }

        [TestMethod()]
        public void StreamWithSecurePasswordUTF8Step10EmptyPasswordTest()
        {
            string password = "";
            var result = GetStreamResult(password, Encoding.UTF8, new byte[1024], 10);
            Assert.AreEqual(JSON_LOGIN_PREFIX + password + JSON_LOGIN_SUFFIX, result);
        }

        [TestMethod()]
        public void StreamWithSecurePasswordUTF8Step10EmptySuffixTest()
        {
            string password = "animalparty", suffix = "";
            var result = GetStreamResult(password, Encoding.UTF8, new byte[1024], 10, suffix: suffix);
            Assert.AreEqual(JSON_LOGIN_PREFIX + password + suffix, result);
        }

        [TestMethod()]
        public void StreamWithSecurePasswordUTF8Step10EmptyPrefixAndSuffixTest()
        {
            string password = "animalparty", prefix = "", suffix = "";
            var result = GetStreamResult(password, Encoding.UTF8, new byte[1024], 10, prefix: prefix, suffix: suffix);
            Assert.AreEqual(prefix + password + suffix, result);
        }

        [TestMethod()]
        public void StreamWithSecurePasswordUTF8Step10EmptyAllTest()
        {
            var result = GetStreamResult(string.Empty, Encoding.UTF8, new byte[1024], 10, prefix: string.Empty, suffix: string.Empty);
            Assert.AreEqual(string.Empty, result);
        }
    }
}