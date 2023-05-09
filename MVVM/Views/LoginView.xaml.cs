using FictionMobile.MVVM.ViewModels;

namespace FictionMobile.MVVM.Views;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

	private void Entry_Unfocused(object sender, FocusEventArgs e)
	{
        ((dynamic)this.BindingContext).Password = ((Entry)sender).Text;
    }

}