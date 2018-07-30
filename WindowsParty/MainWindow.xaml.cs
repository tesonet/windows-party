
using System.Windows;
using WindowsParty.ViewModels;

namespace WindowsParty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IMainViewModel mainViewModel) : base()
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}
