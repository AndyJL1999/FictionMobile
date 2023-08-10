using FictionMobile.Interfaces;
using FictionMobile.MVVM.ViewModels;

namespace FictionMobile.MVVM.Views;

public partial class ReadingView : ContentPage, IHasCollectionView
{
    private ReadingViewModel _vm;
    private double _lastScrollPosition;

    public ReadingView(ReadingViewModel viewModel)
	{
		InitializeComponent();

        _vm = viewModel;

		BindingContext = _vm;
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
        var currentScrollPosition = e.VerticalOffset;
        var scrollDirection = currentScrollPosition.CompareTo(_lastScrollPosition);


        if (scrollDirection == -1 && (e.VerticalDelta > -0.534) == false)
        {
            // Scrolling up
            _vm.IsDownVisible = false;
            _vm.IsUpVisible = true;

        }
        else if (scrollDirection == 1 && (e.VerticalDelta < 0.534) == false)
        {
            // Scrolling down
            _vm.IsUpVisible = false;
            _vm.IsDownVisible = true;
        }

        if(e.VerticalDelta < 0.534 && e.VerticalDelta > -0.534)
        {
            // Stopped scrolling
            _vm.IsUpVisible = false;
            _vm.IsDownVisible = false;
        }

        _lastScrollPosition = currentScrollPosition;
    }
}