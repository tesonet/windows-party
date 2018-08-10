using System;
using System.Windows;
using System.Windows.Input;
using WindowsParty.IServices;
using WindowsParty.ViewModels;

namespace WindowsParty.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void bg_btn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var serversView = new ServersView
            {
                Width = this.Width,
                Height = this.Height,
                Top = this.Top,
                Left = this.Left
            };

            var serverViewModels = ((LoginViewModel)DataContext).GetServers();
            serversView.Servers.ItemsSource = serverViewModels;
            
            this.Hide();
            serversView.Show();
        }
    }
}
