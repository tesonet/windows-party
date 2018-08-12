using System.Globalization;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tesonet.Client.Converters;

namespace Tesonet.Client.Tests.ConvertersTests
{
    [TestClass]
    public class IsCheckedToVisibleConverterTests
    {
        [TestMethod]
        public void ShouldReturnCollapsed_WhenIsCheckedEqualsFalse()
        {
            //arrange
            var converter = new IsCheckedToVisibleConverter();

            //act
            var result = converter.Convert(false, null, null, CultureInfo.InvariantCulture);

            //assert
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod]
        public void ShouldReturnHidden_WhenIsCheckedEqualsNull()
        {
            //arrange
            var converter = new IsCheckedToVisibleConverter();

            //act
            var result = converter.Convert(null, null, null, CultureInfo.InvariantCulture);

            //assert
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod]
        public void ShouldReturnVisible_WhenIsCheckedEqualsTrue()
        {
            //arrange
            var converter = new IsCheckedToVisibleConverter();

            //act
            var result = converter.Convert(true, null, null, CultureInfo.InvariantCulture);

            //assert
            Assert.AreEqual(Visibility.Visible, result);
        }
    }
}