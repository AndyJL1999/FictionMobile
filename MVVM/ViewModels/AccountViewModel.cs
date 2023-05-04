using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FictionMobile.MVVM.Models;
using FictionMobile.MVVM.Views;
using FictionMobile.Resources.Messangers;
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
    private string _newEmail;
    [ObservableProperty]
    private string _newUsername;
    [ObservableProperty]
    private string _oldPassword;
    [ObservableProperty]
    private string _newPassword;
    [ObservableProperty]
    private string _updateText;
    [ObservableProperty]
    private bool _updateVisible;

    private string _passwordHolder;

    private readonly IAPIHelper _apiHelper;

    public AccountViewModel(IAPIHelper apiHelper)
    {
        _apiHelper = apiHelper;
        _passwordHolder = Preferences.Get("UserPassword", "null");

        UpdateVisible = false;
    }

    // Will change this method in the future after modifying FictionHoarder
    [RelayCommand]
    private async void Update()
    {
        try
        {
            UpdateVisible = false;

            if (OldPassword != _passwordHolder)
            {
                throw new Exception("Incorrect password: Old password");
            }

            if(string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassword) ||
                string.IsNullOrEmpty(NewEmail) || string.IsNullOrEmpty(NewUsername))
            {
                throw new Exception("Fill in every field");
            }

            var result = await _apiHelper.UpdateUser(NewUsername, NewEmail, NewPassword);

            UpdateText = result;
            UpdateVisible = true;

            User.Username = NewUsername;
            User.Email = NewEmail;

            OldPassword = string.Empty;
            NewPassword = string.Empty;
        }
        catch(Exception ex)
        {
            UpdateText = ex.Message;
            UpdateVisible = true;
        }
    }

    [RelayCommand]
    private async Task LogOut()
    {
        Preferences.Clear();
        WipeAccountInfo();
        _apiHelper.ClearLoggedInUser();
        await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
    }

    private void WipeAccountInfo()
    {
        NewEmail = string.Empty;
        NewUsername = string.Empty;
    }

    partial void OnUserChanged(UserDisplayModel value)
    {
        NewEmail = value.Email;
        NewUsername = value.Username;
    }
}

