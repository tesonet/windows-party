using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tesonet.Client.Helpers;
using Tesonet.Client.Models;
using Tesonet.Client.Properties;
using Tesonet.Client.Services.NavigationService;
using Tesonet.Client.Services.NavigationService.NavigationData;
using Tesonet.Client.ViewModels.Base;
using Tesonet.Domain.Domain;
using Tesonet.ServerProxy.Services.ServersService;

namespace Tesonet.Client.ViewModels
{
    public class ServersPageViewModel : NavigableViewModel
    {
        private readonly IServersService _serversService;
        private readonly ISettings _settings;

        private ObservableCollection<Server> _servers;

        public ServersPageViewModel(INavigationService navigationService, IServersService serversService, ISettings settings) : base(navigationService)
        {
            _serversService = serversService;
            _settings = settings;
        }

        public ObservableCollection<Server> Servers
        {
            get => _servers;
            set
            {
                _servers = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<CountryServers> CountryServers
        {
            get
            {
                if (Servers == null)
                {
                    return new ObservableCollection<CountryServers>();
                }

                var list = Servers.GroupBy(x => x.Country).Select(x => new CountryServers
                {
                    Country = x.Key,
                    Servers = x.ToList()
                });
                return new ObservableCollection<CountryServers>(list);
            }
        }

        public override async Task InitializeAsync(NavigationData navigationData)
        {
            try
            {
                await ExecuteBusyActionAsync(Initialize, Resources.GetServers);
            }
            catch (Exception e)
            {
                await NavigationService.NavigateToErrorPageAsync(new ErrorPageNavigationData
                {
                    ErrorTitle = Resources.InitializeServersPageError,
                    ErrorMessage = e.Message,
                    NavigatedFromPage = this
                });
            }
        }

        private async Task Initialize()
        {
            Servers = await _serversService.GetServersAsync(_settings.ServerServersUrl, _settings.AuthToken);
            RaisePropertyChanged(() => CountryServers);
        }
    }
}