
namespace CaliburnMicro.LoginTestExternal.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Text;
    using System.Windows;

    using Caliburn.Micro;

    using CaliburnMicro.LoginTestExternal.Model;
    using CaliburnMicro.LoginTestExternal.ViewModels;

    using ExternalControls;


    [Export(typeof(ILoginConductor))]
    public class LoginConductor : ILoginConductor
    {
        #region Fields
        const string token_address = "http://playground.tesonet.lt/v1/tokens";
        const string server_address = "http://playground.tesonet.lt/v1/servers";
        private IEventAggregator events;

       
        private ILoginService loginService;

    
        private IWindowManager windowManager;

        #endregion Fields

        #region Constructors

  
        [ImportingConstructor]
        public LoginConductor(IEventAggregator events, ILoginService loginService, IWindowManager windowManager)
        {
            this.events = events;
            this.loginService = loginService;
            this.windowManager = windowManager;

            this.events.Subscribe(this);
        }

        #endregion Constructors

        #region Methods

      
        public void Handle(LoginEvent message)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Login += new EventHandler<LoginEventArgs>(this.LoginWindow_Login);
            loginWindow.Cancel += new EventHandler(LoginWindow_Cancel);
            loginWindow.ShowDialog();
        }

      
        public void Handle(LogoutEvent message)
        {
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            message.Source.TryClose();
            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
            this.events.Publish(new LoginEvent());
        }

    
        public void Handle(ExitEvent message)
        {
            Application.Current.Shutdown();
        }

        private void LoginWindow_Login(object sender, LoginEventArgs e)
        {
   
            string token = string.Empty;
            token = this.loginService.ValidateUser(e.Username, e.Password,token_address);

            if (!String.IsNullOrEmpty(token))
            {
                ContentViewModel viewModel;
                viewModel = IoC.Get<ContentViewModel>();
                viewModel.Token = token;
                List<Server> servers=this.loginService.GetList(token, server_address);
                events.Publish(new ModelEvents(servers));
                this.windowManager.ShowWindow(viewModel);
            }

            else
            {

                MessageBox.Show("Wrong credentials");
                return;
            }
        }

 
        void LoginWindow_Cancel(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion Methods
    }
}