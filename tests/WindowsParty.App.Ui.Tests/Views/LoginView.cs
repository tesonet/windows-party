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
    public class LoginView : WindowsPartyWindow
    {
        public ServersView Login()
        {
            var usernameTextBoxSearchCriteria = SearchCriteria.ByAutomationId(AutomationIds.UsernameTextBox);
            var usernameTextBox = Retry.For(() => Window.Get<TextBox>(usernameTextBoxSearchCriteria), TimeSpan.FromMilliseconds(500));
            usernameTextBox.Text = "tesonet";

            var passwordTextBoxSearchCriteria = SearchCriteria.ByAutomationId(AutomationIds.PasswordTextBox);
            var passwordTextBox = Retry.For(() => Window.Get<TextBox>(passwordTextBoxSearchCriteria), TimeSpan.FromMilliseconds(500));
            passwordTextBox.Text = "partyanimal";

            var loginButtonCriteria = SearchCriteria.ByAutomationId(AutomationIds.LoginButton);
            var loginButton = Retry.For(() => Window.Get<Button>(loginButtonCriteria), TimeSpan.FromMilliseconds(500));
            loginButton.Click();
            var serversWindow = Retry.For(() => App.GetWindow(AppViews.ServersView), TimeSpan.FromMilliseconds(500));

            return new ServersView(App, serversWindow);
        }

        public LoginView Wait(int miliseconds)
        {
            Thread.Sleep(miliseconds);
            return this;
        }

        public LoginView(Application app, Window window) : base(app, window)
        {
        }
    }
}
