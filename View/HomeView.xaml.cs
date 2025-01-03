using AbsentManagement.ViewModel;

namespace AbsentManagement.View;

public partial class HomeView : ContentPage
{
	public HomeView()
	{
		InitializeComponent();
      
            
       BindingContext = new HomeViewModel(); // Manual assignment
       

    }
}