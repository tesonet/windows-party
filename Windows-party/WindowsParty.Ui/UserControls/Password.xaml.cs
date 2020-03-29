namespace WindowsParty.Ui.UserControls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for Password.xaml
    /// </summary>
    public partial class Password : UserControl
    {
        public static readonly DependencyProperty PasswordTextProperty = DependencyProperty.Register(
            "PasswordText",
            typeof(string),
            typeof(Password),
            new FrameworkPropertyMetadata(null)
            {
                BindsTwoWayByDefault = true
            });

        public Password()
        {
            InitializeComponent();
        }

        public string PasswordText
        {
            get => (string)GetValue(PasswordTextProperty);

            set => SetValue(PasswordTextProperty, value);
        }

        private void PasswordTextBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordText = PasswordTextBox.Password;
        }
    }
}