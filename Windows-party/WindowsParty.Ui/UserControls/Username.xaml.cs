namespace WindowsParty.Ui.UserControls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for Username.xaml
    /// </summary>
    public partial class Username : UserControl
    {
        public static readonly DependencyProperty EditableTextProperty = DependencyProperty.Register(
            "EditableText",
            typeof(string),
            typeof(Username),
            new FrameworkPropertyMetadata(null)
            {
                BindsTwoWayByDefault = true
            });

        public Username()
        {
            InitializeComponent();
        }

        public string EditableText
        {
            get => (string)GetValue(EditableTextProperty);

            set => SetValue(EditableTextProperty, value);
        }
    }
}