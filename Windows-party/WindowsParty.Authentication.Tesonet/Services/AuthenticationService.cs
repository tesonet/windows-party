namespace WindowsParty.Authentication.Tesonet.Services
{
    using System;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using WindowsParty.Domain.Contracts;
    using WindowsParty.Domain.Models;

    internal class AuthenticationService : IAuthenticationService
    {
        private readonly string _endpoint;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _jsonConverterSettings;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<AuthenticationService> logger)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _endpoint = configuration.GetSection("AuthenticationEndpoint").Value;

            _jsonConverterSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public async Task<TokenResult> LogInAsync(Credentials credentials)
        {
            try
            {
                if (UsernameShouldBeSpecified(credentials) || PasswordShouldBeSpecified(credentials))
                {
                    return new TokenResult();
                }

                var jsonCredentials = JsonConvert.SerializeObject(credentials, _jsonConverterSettings);
                var content = new StringContent(jsonCredentials, Encoding.UTF8, MediaTypeNames.Application.Json);
                var response = await _httpClient.PostAsync(_endpoint, content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.Log(LogLevel.Information, $"Log in failed: {responseString}.");
                    return new TokenResult();
                }

                return JsonConvert.DeserializeObject<TokenResult>(responseString, _jsonConverterSettings);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Unexpected error occured.");

                return new TokenResult();
            }
        }

        private static bool PasswordShouldBeSpecified(Credentials credentials)
        {
            return string.IsNullOrWhiteSpace(credentials.Password);
        }

        private static bool UsernameShouldBeSpecified(Credentials credentials)
        {
            return string.IsNullOrWhiteSpace(credentials.Username);
        }
    }
}