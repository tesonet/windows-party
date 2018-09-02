
namespace ExternalControls
{
    using System;
    using System.Windows;

 
    public partial class LoginWindow : Window
    {
    
        public LoginWindow()
        {
            this.InitializeComponent();
        }

       
        public event EventHandler<LoginEventArgs> Login;

        
        public event EventHandler Cancel;

       
        protected void OnLogin(LoginEventArgs e)
        {
            if (this.Login != null)
            {
                this.Login(this, e);
            }
        }

       
        protected void OnCancel(EventArgs e)
        {
            if (this.Cancel != null)
            {
                this.Cancel(this, e);
            }
        }

     
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            this.OnLogin(new LoginEventArgs(this.Username.Text, this.Password.Password));
            this.DialogResult = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.OnCancel(EventArgs.Empty);
            this.DialogResult = false;
        }
    }
}
