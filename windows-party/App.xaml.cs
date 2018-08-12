using System.Windows;
using log4net.Config;

namespace Tesonet.Windows.Party
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            XmlConfigurator.Configure();
            base.OnStartup(e);
        }
    }
}
