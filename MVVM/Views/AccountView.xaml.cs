using FictionMobile.MVVM.ViewModels;

namespace FictionMobile.MVVM.Views;

public partial class AccountView : ContentPage
{
	public AccountView(AccountViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}