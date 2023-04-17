using FictionMobile.MVVM.Models;
using FictionMobile.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace FictionMobile.MVVM.Views;

public partial class StoriesView : ContentPage
{

	public StoriesView(StoriesViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}