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
using System.Windows.Shapes;
using System.ComponentModel;

namespace testio.Login
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            DataContextChanged += (s, e) =>
            {
                if (e.NewValue is LoginViewModel)
                {
                    // pk: Security issue - ugly code to avoid plain text password property
                    var loginViewMode = (LoginViewModel)e.NewValue;
                    loginViewMode.HarvestPassword += () => { return txbPassword.Password; };
                    txbPassword.PasswordChanged += (s1, e1) => { loginViewMode.OnPasswordChanged(); };                    
                }
            };

//#if DEBUG
//            txbPassword.Password = "partyanimal";
//#endif
        }
    }
}
