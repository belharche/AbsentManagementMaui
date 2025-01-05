using AbsentManagement.ViewModel;

namespace AbsentManagement.View;

public partial class StudentAbsencesView : ContentPage
{
	public StudentAbsencesView(StudentAbsencesViewModel studentAbsencesViewModel)
	{
		InitializeComponent();
		BindingContext = studentAbsencesViewModel;
	}
   

}