using AbsentManagement.ViewModel;

namespace AbsentManagement.View;

public partial class NewLessonView : ContentPage
{
	public NewLessonView(NewLessonViewModel newLessonViewModel )
	{
		InitializeComponent();
		BindingContext = newLessonViewModel;
	}
}