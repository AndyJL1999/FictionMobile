using CommunityToolkit.Maui;
using FictionMobile.Extensions;
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
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddApplicationServices();

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
