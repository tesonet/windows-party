using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
