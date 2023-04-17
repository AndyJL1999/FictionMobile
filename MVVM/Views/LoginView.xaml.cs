using FictionMobile.MVVM.ViewModels;

namespace FictionMobile.MVVM.Views;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}