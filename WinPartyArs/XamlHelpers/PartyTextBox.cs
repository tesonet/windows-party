using System;
using System.Windows;
using System.Windows.Controls;

namespace WinPartyArs.XamlHelpers
{
    /// <summary>
    /// Attached properties for TextBox and PasswordBox to containt Image and watermark (Hint).
    /// In order not to reveal password in memory, PasswordChanged using weak event was used, to check only for length.
    /// </summary>
    public static class PartyTextBox
    {
        public static Uri GetImage(DependencyObject obj) => (Uri)obj.GetValue(ImageProperty);

        public static void SetImage(DependencyObject obj, Uri value) => obj.SetValue(ImageProperty, value);

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.RegisterAttached("Image", typeof(Uri), typeof(PartyTextBox), new UIPropertyMetadata(null));

        public static string GetHint(DependencyObject obj) => (string)obj.GetValue(HintProperty);

        public static void SetHint(DependencyObject obj, string value) => obj.SetValue(HintProperty, value);

        public static readonly DependencyProperty HintProperty = 
            DependencyProperty.RegisterAttached("Hint", typeof(string), typeof(PartyTextBox), new UIPropertyMetadata(null, HintChanged));

        public static bool GetPasswordHintVisible(DependencyObject obj) => (bool)obj.GetValue(PasswordHintVisibleProperty);

        public static void SetPasswordHintVisible(DependencyObject obj, bool value) => obj.SetValue(PasswordHintVisibleProperty, value);

        public static readonly DependencyProperty PasswordHintVisibleProperty =
            DependencyProperty.RegisterAttached("PasswordHintVisible", typeof(bool), typeof(PartyTextBox), new UIPropertyMetadata(true));

        private static void HintChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox pb)
                WeakEventManager<PasswordBox, RoutedEventArgs>.AddHandler(pb, nameof(PasswordBox.PasswordChanged), Pb_PasswordChanged);
        }

        private static void Pb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox pb)
                SetPasswordHintVisible(pb, pb.SecurePassword.Length == 0);
        }
    }
}
