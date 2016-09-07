using TestStack.White;
using TestStack.White.UIItems.WindowItems;

namespace WindowsParty.App.Ui.Tests.Views
{
    public class WindowsPartyWindow 
    {
        public readonly Window Window;
        public Application App { get; }

        public WindowsPartyWindow(Application app, Window window)
        {
            Window = window;
            App = app;
        }

    }
}