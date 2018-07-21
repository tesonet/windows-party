using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Tesonet.Windows_party.ViewModel;

namespace Tesonet.Windows_party
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {        
        private void AppOnStartup(object sender, StartupEventArgs e)
        {
            var viewModelLocator = (ViewModelLocator)Resources["ViewModelLocator"];
            viewModelLocator.NavigationService.ShowLoginView();
        }
    }
}
