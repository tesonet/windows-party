using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace testio.Controls.TextBoxes
{
    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach",
            typeof(bool), typeof(PasswordBoxHelper),
            new PropertyMetadata(false, AttachChanged));

        public static readonly DependencyProperty IsPasswordEmptyProperty =
            DependencyProperty.RegisterAttached("IsPasswordEmpty",
            typeof(bool), typeof(PasswordBoxHelper),
            new FrameworkPropertyMetadata(true));

        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }

        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }

        public static bool GetIsPasswordEmpty(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsPasswordEmptyProperty);
        }

        private static void SetIsPasswordEmpty(DependencyObject dp, bool value)
        {
            dp.SetValue(IsPasswordEmptyProperty, value);
        }

        private static void AttachChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            passwordBox.PasswordChanged += (s1, e1) =>
            {
                var oldValue = GetIsPasswordEmpty(passwordBox);
                var newValue = passwordBox.Password.Length == 0;
                if (oldValue != newValue)
                {
                    SetIsPasswordEmpty(passwordBox, newValue);
                }
            };
        }
    }
}
