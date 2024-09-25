using System.Windows;
using System.Windows.Controls;

namespace AdministracijaKorisnika.View
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.LoginViewModel();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                var passwordBox = sender as PasswordBox;
                var loginViewModel = this.DataContext as ViewModel.LoginViewModel;

                if (passwordBox != null && loginViewModel != null)
                {
                    loginViewModel.UserPass = passwordBox.Password;
                }
            }
        }
    }
}
