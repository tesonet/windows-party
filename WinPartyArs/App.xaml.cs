using System.Windows;

namespace WinPartyArs
{
    public partial class App : Application
    {
        private Bootstrapper bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            bootstrapper.OnExit(e);
        }
    }
}
