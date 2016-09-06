using PartyApp.Utilities;

namespace PartyApp.Services
{
	public class AuthorizationToken
	{
		public AuthorizationToken(string value)
		{
			Guard.NotEmpty(value, nameof(value));
			Value = value;
		}

		public string Value { get; }
	}
}
