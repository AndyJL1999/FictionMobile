using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FictionMobile.MVVM.Views;
using Maui_UI_Fiction_Library.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.MVVM.ViewModels;

[QueryProperty(nameof(Username), "Username")]
public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _username;

    public MainViewModel()
    {
        
    }

    [RelayCommand]
    private async Task GoToStories()
    {
        await Shell.Current.GoToAsync(nameof(StoriesView));
    }

    [RelayCommand]
    private async Task GoToAccount()
    {
        await Shell.Current.GoToAsync(nameof(AccountView));
    }

    [RelayCommand]
    private async Task GoToSearch()
    {
        await Shell.Current.GoToAsync(nameof(SearchView));
    }

    [RelayCommand]
    private async Task GoToHistory()
    {
        await Shell.Current.GoToAsync(nameof(HistoryView));
    }
}

