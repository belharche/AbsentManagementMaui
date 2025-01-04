using AbsentManagement.Database;
using AbsentManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AbsentManagement.ViewModel
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly DbService _dbService;
        private string _name;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _errorMessage;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand { get; }
        public ICommand NavigateToLoginCommand { get; }


        public RegisterViewModel(DbService dbService)
        {
            _dbService = dbService;
            RegisterCommand = new Command(async () => await RegisterHandler());
            NavigateToLoginCommand = new Command(async () => await NavigateToLogin());
        }


        // Onclick Handler
        private async Task RegisterHandler()
        {
            ErrorMessage = string.Empty;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = "Email and password cannot be empty.";
                return;
            }
            else if(Password != ConfirmPassword)
            {
                ErrorMessage = "Password should match.";
                return;
            }

            try
            {
                var newUser = new User
                {
                    Email = Email,
                    Password = Password,
                    Name = Name
                };

                await _dbService.InsertUser(newUser);
                // Navigate to Login Screen
                await Shell.Current.GoToAsync("LoginView");
            }
            catch (Exception _)
            {
                ErrorMessage = "An error occurred during Registration. Please try again.";
            }
        }


        private async Task NavigateToLogin()
        {
            try
            {
                if (Shell.Current != null)
                {
                    await Shell.Current.GoToAsync("//LoginView");
                }
                else
                {
                    ErrorMessage = "Navigation service is not initialized";
                    System.Diagnostics.Debug.WriteLine("Shell.Current is null");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Navigation error: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
