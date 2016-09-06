using System;
using System.Net.Http.Headers;
using System.Text;

namespace PartyApp.Utilities
{
	public static class HttpExtensions
	{
		public static string AsString(this HttpRequestHeaders headers)
		{
			Guard.NotNull(headers, nameof(headers));

			var builder = new StringBuilder();
			foreach (var kvp in headers)
			{
				if (kvp.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase))
				{
					//protection from the laziest hackers
					builder.Append($" {kvp.Key} : *******");
					continue;
				}

				string values = string.Join(";", kvp.Value);
				builder.Append($" {kvp.Key} : {values}");
			}

			return builder.ToString();
		}
	}
}
