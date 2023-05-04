using FictionMobile.MVVM.ViewModels;

namespace FictionMobile.MVVM.Views;

public partial class AccountView : ContentPage
{
	public AccountView(AccountViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

	private void passEntry_Unfocused(object sender, FocusEventArgs e)
	{
		((dynamic)this.BindingContext).OldPassword = ((Entry)sender).Text;
	}

	private void newEntry_Unfocused(object sender, FocusEventArgs e)
	{
        ((dynamic)this.BindingContext).NewPassword = ((Entry)sender).Text;
    }
}