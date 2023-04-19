using FictionMobile.MVVM.ViewModels;

namespace FictionMobile.MVVM.Views;

public partial class SearchView : ContentPage
{
	public SearchView(SearchViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}