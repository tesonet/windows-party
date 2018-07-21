using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Tesonet.Windows_party.WpfControls
{
    /// <summary>
    /// Interaction logic for WatermarkTextBox.xaml
    /// </summary>
    public partial class UsernameTextBox : UserControl
    {
        /// <summary>
        /// The text property.
        /// </summary>        
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(UsernameTextBox), new FrameworkPropertyMetadata(string.Empty)
        {
           BindsTwoWayByDefault = true,
           DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        });

        public UsernameTextBox()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }
    }
}
