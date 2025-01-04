using AbsentManagement.ViewModel;

namespace AbsentManagement.View;

public partial class AddStudentView : ContentPage
{
	public AddStudentView(AddStudentViewModel addStudentViewModel)
	{
		InitializeComponent();

		BindingContext = addStudentViewModel;
	}
}