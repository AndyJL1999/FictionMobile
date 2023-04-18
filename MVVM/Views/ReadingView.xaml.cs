using FictionMobile.MVVM.ViewModels;

namespace FictionMobile.MVVM.Views;

public partial class ReadingView : ContentPage
{
	public ReadingView(ReadingViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}