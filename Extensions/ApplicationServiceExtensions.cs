using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using FictionMobile.MVVM.Models;
using FictionMobile.MVVM.ViewModels;
using FictionMobile.MVVM.Views;
using Maui_UI_Fiction_Library.API;
using Maui_UI_Fiction_Library.Models;

namespace FictionMobile.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var mapConfig = new MapperConfiguration(myConfig =>
            {
                myConfig.CreateMap<StoryModel, StoryDisplayModel>();
                myConfig.CreateMap<StoryDisplayModel, StoryModel>();
            });

            var mapper = mapConfig.CreateMapper();

            services.AddSingleton(mapper);
            
            services.AddSingleton<AppShellViewModel>();
            
            services.AddTransient<LoginView>();
            services.AddTransient<LoginViewModel>();
            
            services.AddSingleton<MainView>();
            services.AddSingleton<MainViewModel>();
            
            services.AddScoped<StoriesView>();
            services.AddScoped<StoriesViewModel>();
            
            services.AddScoped<HistoryView>();
            services.AddScoped<HistoryViewModel>();
            
            services.AddTransient<ReadingView>();
            services.AddScoped<ReadingViewModel>();
            
            services.AddScoped<AccountView>();
            services.AddScoped<AccountViewModel>();
            
            services.AddScoped<SearchView>();
            services.AddScoped<SearchViewModel>();
            
            services.AddSingleton<IAPIHelper, APIHelper>();
            services.AddSingleton<IStoryEndpoint, StoryEndpoint>();
            services.AddSingleton<ILoggedInUser, LoggedInUser>();
            
            services.AddSingleton<IMessenger, WeakReferenceMessenger>();

            return services;
        }
    }
}
