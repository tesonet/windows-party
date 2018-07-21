using Newtonsoft.Json;
using Prism.Events;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinPartyArs.Abstracts;
using WinPartyArs.Common;

namespace WinPartyArs.Models
{
    public class TesonetService : TesonetServiceAbstract
    {
        private const string AUTHORIZATION_HEADER_NAME = "Authorization";
        private const string AUTHORIZATION_HEADER_SCHEME = "Bearer";
        private const string TESONET_WRONG_LOGIN_RESPONSE = "Unauthorized";

        /// <summary>
        /// Tesonet server doesn't support charset:UTF16, so just preparing UTF8 bytes to escape JSON characters.
        /// Invalid characters sourc: http://json.org/ string diagram
        /// </summary>
        private readonly Dictionary<char, byte[]> jsonEscapedCharsToBytes = new Dictionary<char, byte[]>
        {
            { '\"', new byte[] { (byte)'\\', (byte)'"' } },  // change " to \"
            { '\\', new byte[] { (byte)'\\', (byte)'\\' } }, // change \ to \\
            /* this probably can't be used in passwords, so will ignore all control characters, uncomment if needed
            {  '/', new byte[] { (byte)'\\', (byte)'/' } },  // change / to \/
            { '\b', new byte[] { (byte)'\\', (byte)'b' } },  // change BACKSPACE to \b
            { '\f', new byte[] { (byte)'\\', (byte)'f' } },  // change FORMFEED to \f
            { '\n', new byte[] { (byte)'\\', (byte)'n' } },  // change NEWLINE to \n
            { '\r', new byte[] { (byte)'\\', (byte)'r' } },  // change CarriageReturn to \r
            { '\t', new byte[] { (byte)'\\', (byte)'t' } },  // change TAB to \t
            */
        };
        
        private HttpClient client = new HttpClient();

        /// <summary>
        /// Base adress can be configuret in AppSettings["PlaygroundBaseUrl"]. This class uses JSON to talk with Tesonet server.
        /// </summary>
        public TesonetService(ILoggerFacade log, IEventAggregator eventAggregator) : base(log, eventAggregator)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["PlaygroundBaseUrl"]);
            _log.Log($"TesonetService() ctor finished initializing with BaseAddress={client.BaseAddress}", Category.Info);
        }

        /// <summary>
        /// To have maximum security, it uses StreamWithSecurePassword, to minimize the risk of reading password from memory.
        /// It forms JSON request and expects token and adds it to future DefaultRequestHeaders.Authorization and returns it.
        /// If encounters message instead, then TesonetServerMessageException is thrown, unless the message is "Unauthorized"
        /// (in that case it just returns null to signal bad username and/or password. If none of the token or message are found,
        /// then TesonetUnexepectedResponseException is thrown with response string. Relative path can be configured using
        /// AppSettings["PlaygroundTokensRelativePath"].
        /// </summary>
        protected override async Task<string> LoginAsync(string username, SecureString password, CancellationToken cancellationToken)
        {
            //await Task.Delay(1000, cancellationToken);
            var content = new StreamContent(new StreamWithSecurePassword(_log, $@"{{""username"":{JsonConvert.ToString(username)},""password"":""",
                password, @"""}", Encoding.UTF8, jsonEscapedCharsToBytes));
            content.Headers.Add("content-type", "application/json");
            string relativeUrl = ConfigurationManager.AppSettings["PlaygroundTokensRelativePath"];
            _log.Log($"TesonetService.LoginAsync() calling PostAsync(relativeUrl: '{relativeUrl}')", Category.Info);
            var r = await client.PostAsync(relativeUrl, content, cancellationToken).ConfigureAwait(false);
            _log.Log($"TesonetService.LoginAsync() calling Content.ReadAsStringAsync()", Category.Debug);
            var s = await r.Content.ReadAsStringAsync().ConfigureAwait(false);
            _log.Log($"TesonetService.LoginAsync() deserializing response", Category.Debug);
            dynamic j = JsonConvert.DeserializeObject(s);
            if (null != j.token)
            {
                _log.Log($"TesonetService.LoginAsync() got token, so adding it to DefaultRequestHeaders.Authorization and cancelling any pending requests", Category.Debug);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AUTHORIZATION_HEADER_SCHEME, j.token.ToString());
                client.CancelPendingRequests();
                return j.token;
            }
            if (null != j.message)
            {
                _log.Log($"TesonetService.LoginAsync() got message ({j.message}) instead of token", Category.Info);
                if (j.message != TESONET_WRONG_LOGIN_RESPONSE)
                    throw new TesonetServerMessageException(j.message.ToString());
                else
                    return null;
            }
            _log.Log($"TesonetService.LoginAsync() got unknown response from server: {s}", Category.Warn);
            throw new TesonetUnexepectedResponseException(s);
        }

        /// <summary>
        /// Relative path can be configured using AppSettings["PlaygroundServersRelativePath"]. 
        /// </summary>
        public override async Task<dynamic> GetServerList(CancellationToken cancellationToken) => await GetSomeDynamicObjectFromServer(
            ConfigurationManager.AppSettings["PlaygroundServersRelativePath"], cancellationToken);

        /// <summary>
        /// Method, than can use some relative URL to form GET request with DefaultRequestHeaders.Authorization filled with token
        /// by LoginAsync(), if it's not filled, throws InvalidOperationException. It parses JSON as dynamic and returns it.
        /// </summary>
        protected async Task<dynamic> GetSomeDynamicObjectFromServer(string relativeUrl, CancellationToken cancellationToken)
        {
            //await Task.Delay(1000, cancellationToken);
            string localToken = _token;
            if (null == localToken || localToken != client.DefaultRequestHeaders.Authorization?.Parameter)
                throw new InvalidOperationException("Cannot get from server while not having correct login token!");
            _log.Log($"TesonetService.GetSomeDynamicObjectFromServer() calling GetAsync(relativeUrl: '{relativeUrl}')", Category.Info);
            var res = await client.GetAsync(relativeUrl, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
            _log.Log($"TesonetService.GetSomeDynamicObjectFromServer() calling Content.ReadAsStringAsync()", Category.Debug);
            var s = await res.Content.ReadAsStringAsync();
            _log.Log($"TesonetService.GetSomeDynamicObjectFromServer() deserializing response", Category.Debug);
            dynamic j = JsonConvert.DeserializeObject(s);
            _log.Log($"TesonetService.GetSomeDynamicObjectFromServer() returning dynamic object", Category.Debug);
            return j;
        }


        /// <summary>
        /// Clears DefaultRequestHeaders.Authorization.
        /// </summary>
        protected override void Logout()
        {
            _log.Log($"TesonetService.Logout() removing DefaultRequestHeaders.Authorization", Category.Debug);
            client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
