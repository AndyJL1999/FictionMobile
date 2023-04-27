using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using FictionMobile.MVVM.Models;
using FictionMobile.MVVM.ViewModels;
using FictionMobile.MVVM.Views;
using Maui_UI_Fiction_Library.API;
using Maui_UI_Fiction_Library.Models;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace FictionMobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        var mapConfig = new MapperConfiguration(myConfig =>
        {
            myConfig.CreateMap<StoryModel, StoryDisplayModel>();
            myConfig.CreateMap<StoryDisplayModel, StoryModel>();
        });

        var mapper = mapConfig.CreateMapper();

        builder.Services.AddSingleton(mapper);

		builder.Services.AddTransient<LoginView>();
        builder.Services.AddTransient<LoginViewModel>();

        builder.Services.AddSingleton<MainView>();
        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddScoped<StoriesView>();
        builder.Services.AddScoped<StoriesViewModel>();

        builder.Services.AddScoped<HistoryView>();
        builder.Services.AddScoped<HistoryViewModel>();

        builder.Services.AddScoped<ReadingView>();
		builder.Services.AddScoped<ReadingViewModel>();

		builder.Services.AddScoped<AccountView>();
        builder.Services.AddScoped<AccountViewModel>();

        builder.Services.AddScoped<SearchView>();
        builder.Services.AddScoped<SearchViewModel>();

		builder.Services.AddSingleton<IAPIHelper, APIHelper>();
        builder.Services.AddSingleton<IStoryEndpoint, StoryEndpoint>();
        builder.Services.AddSingleton<ILoggedInUser, LoggedInUser>();

        builder.Services.AddScoped<IMessenger, WeakReferenceMessenger>();

        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("FictionMobile.appSettings.json");

        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        builder.Configuration.AddConfiguration(config);


        //For **DEBUG/TESTING** purposes **ONLY**
#if ANDROID && DEBUG
        Platforms.Android.DangerousAndroidMessageHandlerEmitter.Register();
        Platforms.Android.DangerousTrustProvider.Register();
#endif

        return builder.Build();
	}
}
