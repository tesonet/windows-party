using Newtonsoft.Json;

namespace WindowsParty.Models
{
	public class Server
	{
		#region Fields

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("distance")]
		public int Distance { get; set; }

		#endregion
	}
}
