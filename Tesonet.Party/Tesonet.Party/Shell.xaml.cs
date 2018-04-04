using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tesonet.Party.Services;
using Tesonet.Party.ViewModels;

namespace Tesonet.Party
{
    public partial class Shell : Window
    {
        public Shell(IShellService viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
