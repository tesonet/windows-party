/// <summary>
/// Server List File
/// </summary>
 namespace Tesonet.ViewModels
{
    using Caliburn.Micro;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Tesonet.Models;
    using Tesonet.Services;

    /// <summary>
    ///   ServerListViewModel File
    /// </summary>    
    public class ServerListViewModel : Screen
    {
        /// <summary>
        /// Gets or sets the authentication token.
        /// </summary>
        /// <value>
        /// The authentication token.
        /// </value>
        public string authToken { get; set; }

        /// <summary>
        ///   The event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        ///   The event aggregator
        /// </summary>
        private readonly IHttpService httpService;

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<Server> Items
        {
            get
            {
                return this.GetServers();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerListViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="httpService">The HTTP service.</param>
        public ServerListViewModel(IEventAggregator eventAggregator, IHttpService httpService)
        {
            this.eventAggregator = eventAggregator;
            this.httpService = httpService;
        }

        /// <summary>
        ///   The event aggregator
        /// </summary>
        public void Logout()
        {
            this.eventAggregator.PublishOnUIThread("Logout");
        }

        /// <summary>
        /// Gets the servers.
        /// </summary>
        /// <returns>Server List</returns>
        public List<Server> GetServers()
        {
            return Task.Run(() => this.Servers()).Result;
        }

        /// <summary>
        /// Serverses this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Server>> Servers()
        {
            return await this.httpService.GetAsync<List<Server>>("http://playground.tesonet.lt/v1/servers", this.authToken);
        }
    }
}
