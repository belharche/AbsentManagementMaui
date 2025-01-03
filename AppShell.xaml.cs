using AbsentManagement.View;
namespace AbsentManagement;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("HomeView", typeof(HomeView));
        Routing.RegisterRoute("LoginView", typeof(LoginView));
        Routing.RegisterRoute("RegistrationView", typeof(RegisterView));
        
        Routing.RegisterRoute("AddStudentView", typeof(AddStudentView));
        GoToAsync("//LoginView");
       
    }

}

