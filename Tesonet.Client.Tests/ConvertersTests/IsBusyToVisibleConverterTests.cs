using System.Globalization;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tesonet.Client.Converters;

namespace Tesonet.Client.Tests.ConvertersTests
{
    [TestClass]
    public class IsBusyToVisibleConverterTests
    {
        [TestMethod]
        public void ShouldReturnHidden_WhenIsBusyEqualsFalse()
        {
            //arrange
            var converter = new IsBusyToVisibleConverter();
            
            //act
            var result = converter.Convert(false, null, null, CultureInfo.InvariantCulture);

            //assert
            Assert.AreEqual(Visibility.Hidden, result);
        }

        [TestMethod]
        public void ShouldReturnHidden_WhenIsBusyEqualsNull()
        {
            //arrange
            var converter = new IsBusyToVisibleConverter();

            //act
            var result = converter.Convert(null, null, null, CultureInfo.InvariantCulture);

            //assert
            Assert.AreEqual(Visibility.Hidden, result);
        }

        [TestMethod]
        public void ShouldReturnVisible_WhenIsBusyEqualsTrue()
        {
            //arrange
            var converter = new IsBusyToVisibleConverter();

            //act
            var result = converter.Convert(true, null, null, CultureInfo.InvariantCulture);

            //assert
            Assert.AreEqual(Visibility.Visible, result);
        }
    }
}
