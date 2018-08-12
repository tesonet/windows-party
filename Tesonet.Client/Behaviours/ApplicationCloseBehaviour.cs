using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Tesonet.Client.Behaviours
{
    public class ApplicationCloseBehaviour : Behavior<Button>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += (sender, e) =>
            {
                Application.Current.Shutdown();
            };
        }
    }
}