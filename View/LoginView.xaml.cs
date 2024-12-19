namespace AbsentManagement.View;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
		BindingContext = new ViewModel.LoginViewModel();
	}
}