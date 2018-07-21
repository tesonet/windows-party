using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WinPartyArs.ViewModels;

namespace WinPartyArs.Views
{
    public partial class Login : UserControl
    {
        public Login() => InitializeComponent();

        /// <summary>
        /// PreviewKeyDown handler on the whole control, to be able to cancel login with Esc keyboard button.
        /// </summary>
        private void LoginControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && DataContext is LoginViewModel lvm && lvm.CancelLoginCommand.CanExecute(null)
                && Keyboard.Modifiers == ModifierKeys.None)
            {
                lvm.CancelLoginCommand.Execute(null);
                e.Handled = true;
            }
        }

        /// <summary>
        /// PreviewKeyDown handler on username Textbox, so it would jump to next control (password) instead of triggering default Log In button.
        /// </summary>
        private void Username_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is UIElement uie && e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.None)
            {
                uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                e.Handled = true;
            }
        }

        /// <summary>
        /// When button with keyboard focus becomes no longer focusable (hidden or disabled), then focus is in weird condition, so we
        /// can handle IsVisibleChanged event here to move focus to next focusable control. It uses dispatcher, so the transition would
        /// happen after other events in the queue, i.e. from Log In button it switches to newly appeared Cancel button, not username TextBox.
        /// </summary>
        private void FocusedButtonBecameInvisibleHandler(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Button b && !b.IsVisible && b.IsKeyboardFocusWithin)
                Dispatcher.BeginInvoke((Action)(() => b.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next))));
        }
    }
}
