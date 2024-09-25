using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using AdministracijaKorisnika.Model;

namespace AdministracijaKorisnika.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<User> _users;
        private User _selectedUser;
        private RelayCommand _saveCommand;
        private RelayCommand _addCommand;

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users)); // Korišćenje nameof umesto hardkodiranog stringa
            }
        }

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(param => SaveUser(), param => CanSave());
                }
                return _saveCommand;
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(param => AddUser());
                }
                return _addCommand;
            }
        }

        public MainViewModel()
        {
            // Inicijalizacija korisnika prilikom kreiranja ViewModel-a
            try
            {
                Users = new ObservableCollection<User>(DatabaseHelper.GetAllUsers());
            }
            catch (Exception ex)
            {
                // Prikazivanje greške korisniku i logovanje u konzolu
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"Error loading users: {ex.Message}");
            }
        }

        private bool CanSave()
        {
            // Proverava da li su podaci za izabranog korisnika validni pre nego što dozvoli snimanje
            return SelectedUser != null && !string.IsNullOrWhiteSpace(SelectedUser.UserName) && !string.IsNullOrWhiteSpace(SelectedUser.UserPass);
        }

        private void SaveUser()
        {
            if (SelectedUser != null)
            {
                try
                {
                    DatabaseHelper.UpdateUser(SelectedUser);
                    MessageBox.Show("User updated successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    // Prikazivanje greške korisniku i logovanje u konzolu
                    MessageBox.Show($"Error updating user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Console.WriteLine($"Error updating user: {ex.Message}");
                }
            }
        }

        private void AddUser()
        {
            var newUser = new User { UserName = "New User", UserPass = "password", IsAdmin = false };
            try
            {
                DatabaseHelper.AddUser(newUser);
                Users.Add(newUser);
                SelectedUser = newUser; // Postavlja novog korisnika kao selektovanog
                MessageBox.Show("New user added successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // Prikazivanje greške korisniku i logovanje u konzolu
                MessageBox.Show($"Error adding user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"Error adding user: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
