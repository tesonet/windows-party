using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace WindowsParty.App.UnitTests
{
    public class AutoTestDataAttribute : AutoDataAttribute
    {
        public AutoTestDataAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
