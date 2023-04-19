using FictionMobile.MVVM.ViewModels;

namespace FictionMobile.MVVM.Views;

public partial class HistoryView : ContentPage
{
	public HistoryView(HistoryViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}