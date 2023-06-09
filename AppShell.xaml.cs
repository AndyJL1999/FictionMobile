﻿using FictionMobile.MVVM.ViewModels;
using FictionMobile.MVVM.Views;

namespace FictionMobile;

public partial class AppShell : Shell
{
	public AppShell(AppShellViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;

		Routing.RegisterRoute(nameof(MainView), typeof(MainView));
        Routing.RegisterRoute(nameof(StoriesView), typeof(StoriesView));
        Routing.RegisterRoute(nameof(HistoryView), typeof(HistoryView));
        Routing.RegisterRoute(nameof(ReadingView), typeof(ReadingView));
        Routing.RegisterRoute(nameof(AccountView), typeof(AccountView));
        Routing.RegisterRoute(nameof(SearchView), typeof(SearchView));
    }

}
