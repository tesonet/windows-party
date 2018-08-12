using System.Threading.Tasks;
using Tesonet.Client.ViewModels.Base;

namespace Tesonet.Client.Services.NavigationService
{
    public interface INavigationService
    {
        Task NavigateToPageAsync(NavigableViewModel navigationPage, NavigationData.NavigationData navigationData);
        Task NavigateToLoginPageAsync(NavigationData.NavigationData navigationData);
        Task NavigateToMainPageAsync(NavigationData.NavigationData navigationData);
        Task NavigateToServersPageAsync(NavigationData.NavigationData navigationData);
        Task NavigateToSettingsPageAsync(NavigationData.NavigationData navigationData);
        Task NavigateToErrorPageAsync(NavigationData.NavigationData navigationData);
    }
}