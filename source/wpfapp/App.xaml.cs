using System.Windows;
using Unity;

namespace tesonet.windowsparty.wpfapp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Bootstrapper.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = (Window)Bootstrapper.Container.Resolve<IMainWindow>();
            MainWindow.ShowDialog();
        }
    }
}
