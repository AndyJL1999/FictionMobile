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
public partial class AccountViewModel : BaseViewModel
{
    [ObservableProperty]
    private UserDisplayModel _user;
    [ObservableProperty]
    private string _oldPassword;
    [ObservableProperty]
    private string _newPassword;

    private readonly IAPIHelper _apiHelper;

    public AccountViewModel(IAPIHelper apiHelper)
    {
        _apiHelper = apiHelper;
    }

    [RelayCommand]
    private async Task LogOut()
    {
        WipeAccountInfo();
        _apiHelper.ClearLoggedInUser();
        await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
    }

    private void WipeAccountInfo()
    {
        User.Email = string.Empty;
        User.Username = string.Empty;
    }
}

