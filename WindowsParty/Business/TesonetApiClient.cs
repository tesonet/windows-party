using EnsureThat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WindowsParty.Logging;
using WindowsParty.Models;

namespace WindowsParty.Business
{
	public class TesonetApiClient
	{
		private const string ServersUrl = "http://playground.tesonet.lt/v1/servers";
		private const string AuthorizationUrl = "http://playground.tesonet.lt/v1/tokens";

		private readonly HttpClient _client;
		private readonly ILogger _log;

		public TesonetApiClient(ILogger log)
		{
			EnsureArg.IsNotNull(log, nameof(log));

			_log = log;
			_client = new HttpClient();
		}

		public async Task<AutorizationResponse> AuthorizeAsync(string username, string password)
		{
			HttpResponseMessage response=null;
			try
			{
				var request = new AuthorizationRequest(username, password);
				response = await _client.PostAsync(AuthorizationUrl, CreateJsonContent(request));

				response.EnsureSuccessStatusCode();
				var tokenJson = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<AutorizationResponse>(tokenJson);
			}
			catch (HttpRequestException ex) when (response?.StatusCode == HttpStatusCode.Unauthorized)
			{
				_log.Warning("Failed to get authorization token from url: {0}. Exception: {1}", AuthorizationUrl, ex.Message);
				return new AutorizationResponse(string.Empty);
			}
			catch (Exception ex)
			{
				_log.Warning("Failed to get authorization token from url: {0}. Exception: {1}", AuthorizationUrl, ex.Message);
				throw;
			}
		}

		public async Task<ICollection<Server>> GetServersAsync(string token)
		{
			try
			{
				var requestMessage = new HttpRequestMessage(HttpMethod.Get, ServersUrl);
				requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

				var response = await _client.SendAsync(requestMessage);
				response.EnsureSuccessStatusCode();

				var responseString = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<ICollection<Server>>(responseString);
			}
			catch (Exception ex)
			{
				_log.Warning("Failed to get servers list from url: {0}. Exception: {1}", ServersUrl, ex.Message);
				throw;
			}
		}

		private HttpContent CreateJsonContent(object content) => new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
	}
}
