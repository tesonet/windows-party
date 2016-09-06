using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using PartyApp.Log;
using PartyApp.Services;
using PartyApp.Utilities;

namespace PartyApp.Clients
{
	public class ServersClient : IServersProvider
	{
		private static readonly Uri ApiUrl = new Uri("http://playground.tesonet.lt/v1/servers");
		private readonly IAppLogger _appLogger;

		public ServersClient(IAppLogger appLogger)
		{
			Guard.NotNull(appLogger, nameof(appLogger));
			_appLogger = appLogger;
		}

		public async Task<IEnumerable<Server>> GetServersAsync(
			AuthorizationToken authorizationToken,
			CancellationToken cancellationToken)
		{
			Guard.NotNull(authorizationToken, nameof(authorizationToken));
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Add(
					"Authorization",
					$"Bearer {authorizationToken.Value}");
				client.DefaultRequestHeaders.Accept.Add(
					new MediaTypeWithQualityHeaderValue("application/json"));

				LogRequest(client.DefaultRequestHeaders);

				HttpResponseMessage response = await client.GetAsync(ApiUrl, cancellationToken);
				LogResponse(response);

				try
				{
					response.EnsureSuccessStatusCode();
				}
				catch (Exception ex)
				{
					_appLogger.WriteException(ex);
					throw;
				}

				var servers = await response.Content.ReadAsAsync<List<Server>>();
				_appLogger.WriteInfo($"Obtained {servers.Count} servers");

				return servers;
			}
		}

		private void LogRequest(HttpRequestHeaders headers)
		{
			if (!_appLogger.IsEnabled)
				return;

			_appLogger.WriteInfo(
				$"Sending GET to {ApiUrl}. Headers: " + headers.AsString());
		}

		private void LogResponse(HttpResponseMessage response)
		{
			if (!_appLogger.IsEnabled)
				return;

			_appLogger.WriteInfo(
				$"Got response: {Environment.NewLine} " + response.ToString());
		}
	}
}
