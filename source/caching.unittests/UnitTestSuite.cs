using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace tesonet.windowsparty.caching.unittests
{
    [TestClass]
    public class UnitTestSuite
    {
        [TestMethod]
        public void Cache_WhenDateTimeOffsetIsZero_ShouldDropValueFromCache()
        {
            // arrange
            var cache = new Cache<int>();

            // assert
            cache.Add("key-1", 1, new DateTimeOffset(DateTime.Now));
            var isInCache = cache.TryGet("key-1", out int value);

            // assert
            Assert.IsFalse(isInCache);
            Assert.AreEqual(default(int), value);
        }

        [TestMethod]
        public void Cache_ShouldReturnExpectedValue()
        {
            // arrange
            var cache = new Cache<int>();

            // assert
            cache.Add("key-1", 1, new DateTimeOffset(DateTime.Now.AddSeconds(1)));
            var isInCache = cache.TryGet("key-1", out int value);

            // assert
            Assert.IsTrue(isInCache);
            Assert.AreEqual(1, value);
        }
    }
}
