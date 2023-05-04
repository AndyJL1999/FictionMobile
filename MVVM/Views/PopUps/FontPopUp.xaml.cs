using CommunityToolkit.Maui.Views;
using FictionMobile.MVVM.ViewModels;

namespace FictionMobile.MVVM.Views.PopUps;

public partial class FontPopUp : Popup
{
	public FontPopUp(ReadingViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}