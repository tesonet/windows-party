using System.Reflection;
using System.Windows;

namespace WPFApp
{
    public partial class App : Application
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public App()
        {
            InitializeComponent();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            base.OnStartup(e);
        }
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            log.Info("        =============  Party is over  =============        ");
        }
    }
}
