using AbsentManagement.ViewModel;

namespace AbsentManagement.View;

public partial class RegisterView : ContentPage
{
	public RegisterView(RegisterViewModel registerViewModel)
	{
		InitializeComponent();
		BindingContext = registerViewModel;

	
    }
}