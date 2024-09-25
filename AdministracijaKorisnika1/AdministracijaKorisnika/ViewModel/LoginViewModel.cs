using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AdministracijaKorisnika.Model;
using AdministracijaKorisnika.View;

namespace AdministracijaKorisnika.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _userName;
        private string _userPass;
        private RelayCommand _loginCommand;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public string UserPass
        {
            get { return _userPass; }
            set
            {
                _userPass = value;
                OnPropertyChanged("UserPass");
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(param => Login(), param => CanLogin());
                }
                return _loginCommand;
            }
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(UserPass);
        }

        private void Login()
        {
            Console.WriteLine($"Login attempt: Username: {UserName}, Password: {UserPass}");

            var user = DatabaseHelper.GetUser(UserName, UserPass);
            if (user != null)
            {
                Console.WriteLine($"Login successful for user: {user.UserName}");

                var mainWindow = new MainWindow();
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();

                // Zatvaranje trenutnog Login prozora
                Application.Current.Windows
                    .OfType<Window>()
                    .FirstOrDefault(w => w is LoginWindow)?.Close();
            }
            else
            {
                Console.WriteLine("Login failed. User not found.");
                MessageBox.Show("Login failed. Please try again.");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
