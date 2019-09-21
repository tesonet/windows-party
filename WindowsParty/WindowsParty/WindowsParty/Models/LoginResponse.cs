using Newtonsoft.Json;

namespace WindowsParty.Models
{
	public class LoginResponse
	{
		#region Fields

		[JsonProperty("token")]
		public string Token { get; set; }

		#endregion
	}
}
