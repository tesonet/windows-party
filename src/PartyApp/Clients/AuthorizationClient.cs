using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PartyApp.Log;
using PartyApp.Properties;
using PartyApp.Services;
using PartyApp.Utilities;

namespace PartyApp.Clients
{
	public class AuthorizationClient : IAuthorizer
	{
		private static readonly Uri ApiUrl = new Uri("http://playground.tesonet.lt/v1/tokens");
		private readonly IAppLogger _appLogger;

		public AuthorizationClient(IAppLogger appLogger)
		{
			Guard.NotNull(appLogger, nameof(appLogger));
			_appLogger = appLogger;
		}

		public async Task<AuthorizationResult> AuthorizeAsync(
			string userName,
			string password,
			CancellationToken cancellationToken)
		{
			using (var client = new HttpClient())
			{
				_appLogger.WriteInfo($"Sending POST to {ApiUrl}");

				var response = await client.PostAsJsonAsync(ApiUrl,
					new Credentials()
					{
						UserName = userName,
						Password = password
					}, cancellationToken);

				try
				{
					response.EnsureSuccessStatusCode();
				}
				catch (HttpRequestException ex) when (response.StatusCode == HttpStatusCode.Unauthorized)
				{
					_appLogger.WriteException(ex);
					return new AuthorizationResult(Resources.FailedToLogin);
				}
				catch (Exception ex)
				{
					_appLogger.WriteException(ex);
					throw;
				}

				LogResponse(response);
				var authorization = await response.Content.ReadAsAsync<Authorization>();
				return new AuthorizationResult(new AuthorizationToken(authorization.Token));
			}
		}

		private void LogResponse(HttpResponseMessage response)
		{
			if (!_appLogger.IsEnabled)
				return;

			_appLogger.WriteInfo(
				$"Got response: {Environment.NewLine} " + response.ToString());
		}

		private class Credentials
		{
			//the api needs properties in lowercase..
			[JsonProperty("username")]
			public string UserName { get; set; }

			[JsonProperty("password")]
			public string Password { get; set; }
		}

		private class Authorization
		{
			public string Token { get; set; }
		}
	}
}
