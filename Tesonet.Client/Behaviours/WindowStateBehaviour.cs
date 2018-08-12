using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Tesonet.Client.Behaviours
{
    public class WindowStateBehaviour : Behavior<Button>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += (sender, e) =>
            {
                var parent = Application.Current.MainWindow;
                switch (parent.WindowState)
                {
                    case WindowState.Maximized:
                        parent.WindowState = WindowState.Normal;
                        break;
                    case WindowState.Normal:
                        parent.WindowState = WindowState.Maximized;
                        break;
                    case WindowState.Minimized:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };
        }
    }
}