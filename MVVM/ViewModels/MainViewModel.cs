using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FictionMobile.MVVM.Models;
using FictionMobile.MVVM.Views;
using Maui_UI_Fiction_Library.API;
using Maui_UI_Fiction_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.MVVM.ViewModels;

[QueryProperty(nameof(User), "User")]
public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    private UserDisplayModel _user;

    public MainViewModel()
    {
        
    }

    [RelayCommand]
    private async Task GoToStories()
    {
        await Shell.Current.GoToAsync(nameof(StoriesView), true, new Dictionary<string, object>
        {
            { nameof(User), User }
        });
    }

    [RelayCommand]
    private async Task GoToAccount()
    {
        await Shell.Current.GoToAsync(nameof(AccountView), true, new Dictionary<string, object>
        {
            { nameof(User), User }
        });
    }

    [RelayCommand]
    private async Task GoToSearch()
    {
        await Shell.Current.GoToAsync(nameof(SearchView));
    }

    [RelayCommand]
    private async Task GoToHistory()
    {
        await Shell.Current.GoToAsync(nameof(HistoryView), true, new Dictionary<string, object>
        {
            { nameof(User), User }
        });
    }

}

