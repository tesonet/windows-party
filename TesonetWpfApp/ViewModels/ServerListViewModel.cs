using Prism.Regions;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesonetWpfApp.Business.Models;
using System.Windows.Input;
using Prism.Commands;
using TesonetWpfApp.Business;

namespace TesonetWpfApp.ViewModels
{
    public class ServerListViewModel : BaseViewModel
    {
        private string _token;
        private readonly IRegionManager _regionManager;
        private readonly ITesonetService _tesonetService;

        public ICollection<Server> Servers { get; set; }
        public ICommand Logout { get; }

        public ServerListViewModel(ITesonetService tesonetService, IRegionManager regionManager)
        {
            _tesonetService = tesonetService;
            _regionManager = regionManager;

            Logout = new DelegateCommand(LogoutAction);
        }

        private void LogoutAction()
        {
            _regionManager.Regions[CONTENT_REGION].NavigationService.Journal.GoBack();
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _token = navigationContext.Parameters["token"] as string;

            Servers = await GetServers(_token);
        }

        private async Task<ICollection<Server>> GetServers(string token)
        {
            return await _tesonetService.GetServers(token);
        }
    }
}
