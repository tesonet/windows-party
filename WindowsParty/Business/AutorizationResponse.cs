using Newtonsoft.Json;

namespace WindowsParty.Business
{
	[JsonObject]
	public class AutorizationResponse
	{
		public AutorizationResponse(string token)
		{
			Token = token;
		}

		[JsonProperty("token")]
		public string Token { get; set; }

		public bool Success => !string.IsNullOrEmpty(Token);
	}
}
