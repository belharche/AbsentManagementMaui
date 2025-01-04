

using AbsentManagement.ViewModel;

namespace AbsentManagement.View;

public partial class AbsenceView : ContentPage
{
	public AbsenceView(AbsenceViewModel absenceViewModel)
	{
		InitializeComponent();
		BindingContext = absenceViewModel;
	}
}