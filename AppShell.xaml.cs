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
        Routing.RegisterRoute("AbsenceView", typeof(AbsenceView));
        Routing.RegisterRoute("AddStudentView", typeof(AddStudentView));
        Routing.RegisterRoute("SearchView", typeof(SearchView));
        Routing.RegisterRoute("NewLessonView", typeof(NewLessonView));
        Routing.RegisterRoute("StudentAbsencesView", typeof(StudentAbsencesView));
        GoToAsync("//LoginView");
       
    }

}

