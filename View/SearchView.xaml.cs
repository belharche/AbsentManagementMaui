using AbsentManagement.ViewModel;

namespace AbsentManagement.View;

public partial class SearchView : ContentPage
{
    public SearchView(SearchViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel; // Set the BindingContext to the injected ViewModel
    }
}