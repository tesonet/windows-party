using System.Windows;
using WindowsParty.Messages;
using WindowsParty.Utils;
using WindowsParty.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using Unity;
using Unity.Resolution;

namespace WindowsParty
{
    public partial class App
    {
        private string _token;

        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Configuration.ConfigureIoC();

            //Register all views:
            ViewForAttribute.ProceedRelatedAssemblies();

            Messenger.Default.Register<AuthorizationMessage>(this, AuthorizationMessageHandler);


            MainWindow = new MainWindow();
            MainWindow.Show();

            AuthorizationMessageHandler(new AuthorizationMessage(_token));
        }

        private void AuthorizationMessageHandler(AuthorizationMessage msg)
        {
            _token = msg.Token;
            IViewModel vm;

            if (msg.Authorized)
                vm = Configuration.Container.Resolve<IServerListViewModel>(new ParameterOverride("token", _token));
            else
                vm = Configuration.Container.Resolve<ILoginViewModel>();

            Dispatcher.InvokeAsync(() => MainWindow.DataContext = vm);
        }
    }
}
