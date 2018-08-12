using EnsureThat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsParty.Business;
using WindowsParty.Models;

namespace WindowsParty.Services
{
	public class ServersService : IServersService
	{
		private string _token;
		private readonly TesonetApiClient _client;

		public ServersService(TesonetApiClient tesonetClient)
		{
			EnsureArg.IsNotNull(tesonetClient, nameof(tesonetClient));

			_client = tesonetClient;
		}

		public async Task<bool> AuthorizeAsync(string username, string password)
		{
			var authorizationResponse = await _client.AuthorizeAsync(username, password);
			_token = authorizationResponse.Token;
			return authorizationResponse.Success;
		}

		public async Task<ICollection<Server>> GetServersAsync()
		{
			if (string.IsNullOrEmpty(_token))
				throw new InvalidOperationException("You are not authorized.");

			return await _client.GetServersAsync(_token);
		}
	}
}
