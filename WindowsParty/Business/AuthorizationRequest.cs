using EnsureThat;
using Newtonsoft.Json;

namespace WindowsParty.Business
{
	[JsonObject]
	public class AuthorizationRequest
	{
		public AuthorizationRequest(string username, string password)
		{
			EnsureArg.IsNotNullOrEmpty(username, nameof(username));
			EnsureArg.IsNotNullOrEmpty(password,nameof(password));

			Username = username;
			Password = password;
		}

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }
	}
}
