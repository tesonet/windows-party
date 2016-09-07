using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsParty.Infrastructure.Navigation;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;

namespace WindowsParty.Infrastructure.Tests
{
    [TestFixture]
    public class TitleResolverTests
    {
        private TitleResolver _sut;

        [SetUp]
        public void Init()
        {
            _sut = new TitleResolver();
        }

        [Test, AutoData]
        public void ChangeTitle_RaisesPropertyChanged(string viewName)
        {
            var wasCalled = false;
            _sut.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_sut.CurrentTitle))
                {
                    wasCalled = true;
                }
            };

            _sut.ChangeTitle(viewName);

            Assert.True(wasCalled);
        }

        [Test, AutoData]
        public void ChangeTitle_ChangesCurrentTitle(string expectedViewName)
        {
            _sut.ChangeTitle(expectedViewName);

            Assert.AreEqual(expectedViewName, _sut.CurrentTitle);
        }

    }
}
