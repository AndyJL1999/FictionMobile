using FictionMobile.Interfaces;
using FictionMobile.MVVM.ViewModels;

namespace FictionMobile.MVVM.Views;

public partial class ReadingView : ContentPage, IHasCollectionView
{
    private double _lastScrollPosition;

    public ReadingView(ReadingViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

	public CarouselView CarouselView => carouselView;
    public CollectionView CollectionView => CarouselView.VisibleViews[0] as CollectionView;

    protected override void OnBindingContextChanged()
    {
        if (BindingContext is IHasCollectionViewModel hasCollectionViewModel)
        {
            hasCollectionViewModel.View = this;
        }
        base.OnBindingContextChanged();
    }

    private void collectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        var vm = (ReadingViewModel)BindingContext;

        var currentScrollPosition = e.VerticalOffset;
        var scrollDirection = currentScrollPosition.CompareTo(_lastScrollPosition);


        if (scrollDirection == -1 && (e.VerticalDelta > -0.534) == false)
        {
            // Scrolling up
            vm.IsDownVisible = false;
            vm.IsUpVisible = true;

        }
        else if (scrollDirection == 1 && (e.VerticalDelta < 0.534) == false)
        {
            // Scrolling down
            vm.IsUpVisible = false;
            vm.IsDownVisible = true;
        }

        if(e.VerticalDelta < 0.534 && e.VerticalDelta > -0.534)
        {
            // Stopped scrolling
            vm.IsUpVisible = false;
            vm.IsDownVisible = false;
        }

        Console.WriteLine(e.VerticalDelta);
        _lastScrollPosition = currentScrollPosition;
    }
}