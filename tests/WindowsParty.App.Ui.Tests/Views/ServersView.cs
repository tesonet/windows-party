using System;
using System.Threading;
using WindowsParty.Infrastructure;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.Utility;

namespace WindowsParty.App.Ui.Tests.Views
{
    public class ServersView : WindowsPartyWindow
    {

        public LoginView Logout()
        {
            var logoutButtonCriteria = SearchCriteria.ByAutomationId(AutomationIds.LogoutButton);
            var logoutButton = Retry.For(() => Window.Get<Button>(logoutButtonCriteria), TimeSpan.FromMilliseconds(500));
            logoutButton.Click();
            var loginWindow = Retry.For(() => App.GetWindow(AppViews.LoginView), TimeSpan.FromMilliseconds(500));

            return new LoginView(App, loginWindow);
        }

        public ServersView Wait(int miliseconds)
        {
            Thread.Sleep(miliseconds);
            return this;
        }

        public ListView GetServerList()
        {
            var criteria = SearchCriteria.ByAutomationId(AutomationIds.ServerList);
            var serverList = Retry.For(() => Window.Get<ListView>(criteria), TimeSpan.FromMilliseconds(500));
            return serverList;
        }

        public ServersView(Application app, Window window) : base(app, window)
        {
        }
    }
}