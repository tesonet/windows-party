using System.Windows;
using Unity;

namespace WindowsParty
{
    public partial class App
    {
        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Configuration.ConfigureIoC();
        }
    }
}
