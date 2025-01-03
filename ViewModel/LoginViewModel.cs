using System.Windows.Input;
using System.Threading.Tasks;
using AbsentManagement.Database;
using AbsentManagement.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace AbsentManagement.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly DbService _dbService;
        private string _email;
        private string _password;
        private string _errorMessage;

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

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegistrationCommand { get; }

        public LoginViewModel(DbService dbService)
        {
            _dbService = dbService;
            LoginCommand = new Command(async () => await LoginHandler());
            NavigateToRegistrationCommand = new Command(async () => await NavigateToRegistration());

        }

        private async Task LoginHandler()
        {
            ErrorMessage = string.Empty;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Email and password cannot be empty.";
                return;
            }

            try
            {
                // Check if the user exists in the database
                var user = await _dbService.GetUsers()
                    .ContinueWith(task => task.Result.FirstOrDefault(u => u.Email == Email && u.Password == Password));

                if (user == null)
                {
                    ErrorMessage = "Invalid email or password.";
                }
                else
                {
                    try
                    {
                        // This part of navigation contains an error (TO BE FIXED)
                        await Shell.Current.GoToAsync("HomeView");
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = "An error Occurred while navigation";
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessage = "An error occurred during login. Please try again.";
            }
        }
        private async Task NavigateToRegistration()
        {
            try
            {
                if (Shell.Current != null)
                {
                    await Shell.Current.GoToAsync("RegistrationView");
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