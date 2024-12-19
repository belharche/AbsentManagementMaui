using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsentManagement.ViewModel
{
    public class LoginViewModel
    {
        public String Email { get; set; }
        public String Password { get; set; }

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(HandleLogin);
        }

        private async void HandleLogin()
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Login in not implemented yet", "OK");
        }
    }
}
