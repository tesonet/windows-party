using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
