using FictionMobile.Interfaces;
using FictionMobile.MVVM.ViewModels;

namespace FictionMobile.MVVM.Views;

public partial class ReadingView : ContentPage, IHasCollectionView
{
	public ReadingView(ReadingViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

	public CarouselView CarouselView => carouselView;

    protected override void OnBindingContextChanged()
    {
        if (BindingContext is IHasCollectionViewModel hasCollectionViewModel)
        {
            hasCollectionViewModel.View = this;
        }
        base.OnBindingContextChanged();
    }
}