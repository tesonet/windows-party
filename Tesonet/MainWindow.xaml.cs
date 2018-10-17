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

namespace Tesonet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataBinding dataBind;
        IViewer viewModel;
        public MainWindow()
        {
            InitializeComponent();
            dataBind = new DataBinding();
            this.DataContext = dataBind;
            viewModel = new VM(dataBind);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.LogIn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
