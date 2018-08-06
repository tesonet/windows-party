using System.Windows;
using WindowsParty.ApiServices;

namespace WindowsParty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPlaygroundService _service;

        public MainWindow()
        {
            InitializeComponent();

            _service = new PlaygroundService();
        }
    }
}
