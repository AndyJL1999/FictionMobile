using FictionMobile.MVVM.Views;

namespace FictionMobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(MainView), typeof(MainView));
        Routing.RegisterRoute(nameof(StoriesView), typeof(StoriesView));
    }
}
