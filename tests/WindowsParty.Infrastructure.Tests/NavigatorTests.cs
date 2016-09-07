using System;
using WindowsParty.Infrastructure.Navigation;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Prism.Regions;

namespace WindowsParty.Infrastructure.Tests
{
    [TestFixture]
    public class NavigatorTests
    {
        private Mock<IRegionManager> _regionManagerMock;
        private Mock<ITitleResolver> _titleResolverMock;
        private Navigator _sut  ;

        [SetUp]
        public void Init()
        {
            _regionManagerMock = new Mock<IRegionManager>();
            _titleResolverMock = new Mock<ITitleResolver>();

            _sut= new Navigator(_regionManagerMock.Object,_titleResolverMock.Object);
        }

        [Test, AutoData]
        public void GoTo_NavigatesToRequiredView(string viewName)
        {
            _sut.GoTo(viewName);

            _regionManagerMock.Verify(t => t.RequestNavigate(Regions.MainRegion, new Uri(viewName, UriKind.Relative)));
        }

        [Test, AutoData]
        public void GoToWithParameters_NavigatesToRequiredViewWithParameters(string viewName, NavigationParameters parameters)
        {
            _sut.GoTo(viewName, parameters);

            _regionManagerMock.Verify(t => t.RequestNavigate(Regions.MainRegion, new Uri(viewName, UriKind.Relative),parameters));
        }

        [Test, AutoData]
        public void GoTo_AlertsOfTitleChange(string viewName)
        {
            _sut.GoTo(viewName);

            _titleResolverMock.Verify(t=>t.ChangeTitle(viewName));
        }
    }
}
