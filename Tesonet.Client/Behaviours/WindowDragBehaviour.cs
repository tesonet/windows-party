using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Tesonet.Client.Behaviours
{
    public class WindowDragBehaviour : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            AssociatedObject.MouseMove += (sender, e) =>
            {
                var isAllowedToDrag = e.LeftButton == MouseButtonState.Pressed &&
                                      Application.Current.MainWindow.WindowState == WindowState.Normal;
                if (isAllowedToDrag)
                {
                    Application.Current.MainWindow.DragMove();
                }
            };

            AssociatedObject.MouseDown += (sender, args) =>
            {
                if (args.LeftButton != MouseButtonState.Pressed)
                {
                    return;
                }

                if (args.ClickCount != 2)
                {
                    return;
                }

                switch (Application.Current.MainWindow.WindowState)
                {
                    case WindowState.Normal:
                        Application.Current.MainWindow.WindowState = WindowState.Maximized;
                        break;
                    case WindowState.Maximized:
                        Application.Current.MainWindow.WindowState = WindowState.Normal;
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