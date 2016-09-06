using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;

namespace PartyApp.Test.Framework
{
	public class AutoMoqDataAttribute : AutoDataAttribute
	{
		public AutoMoqDataAttribute()
			: base(new PartyAppFixture()
				.Customize(new AutoMoqCustomization()))
		{
		}
	}
}
