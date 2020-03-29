namespace WindowsParty.Repository.Tesonet.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using WindowsParty.Domain.Contracts;
    using WindowsParty.Domain.Entities;
    using WindowsParty.Domain.Models;

    public class ServersQueryService : IServerQueryService
    {
        private readonly string _endpoint;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _jsonConverterSettings;
        private readonly ILogger<ServersQueryService> _logger;

        public ServersQueryService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<ServersQueryService> logger)
        {
            _logger = logger;

            _httpClient = httpClientFactory.CreateClient();
            _endpoint = configuration.GetSection("ServersEndpoint").Value;

            _jsonConverterSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public async Task<IList<Server>> GetAsync(TokenResult token)
        {
            try
            {
                if (TokenShouldBeProvided(token))
                {
                    return new List<Server>();
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                var response = await _httpClient.GetAsync(_endpoint);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.Log(LogLevel.Information, $"Get servers failed: {responseString}.");
                    return new List<Server>();
                }
                
                return JsonConvert.DeserializeObject<IList<Server>>(responseString, _jsonConverterSettings);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Unexpected error occured.");

                return new List<Server>();
            }
        }

        private static bool TokenShouldBeProvided(TokenResult token)
        {
            return string.IsNullOrWhiteSpace(token.Token);
        }
    }
}