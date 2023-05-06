﻿using FictionMobile.MVVM.ViewModels;

namespace FictionMobile;

public partial class App : Application
{
	public App(AppShellViewModel vm)
	{
		InitializeComponent();

		MainPage = new AppShell(vm);
	}
}
