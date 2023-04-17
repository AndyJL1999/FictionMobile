using FictionMobile.MVVM.ViewModels;

namespace FictionMobile.MVVM.Views;

public partial class MainView : ContentPage
{
	public MainView(MainViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}