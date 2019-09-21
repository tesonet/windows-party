using Newtonsoft.Json;

namespace WindowsParty.Models
{
	public class Login
	{
		#region Fields

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }

		#endregion
	}
}
