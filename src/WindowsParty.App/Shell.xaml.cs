using System.Windows;
using WindowsParty.Infrastructure.Navigation;

namespace WindowsParty.App
{
    public partial class Shell : Window
    {
        public Shell(ITitleResolver titleResolver)
        {
            InitializeComponent();
            DataContext = titleResolver;
        }
    }
}
