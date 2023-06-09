﻿using CommunityToolkit.Mvvm.ComponentModel;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.MVVM.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _username;
    [ObservableProperty]
    private string _email;
    [ObservableProperty]
    private string _password;
    [ObservableProperty]
    private string _errorMessage;
    [ObservableProperty]
    private string _resultMessage;
    [ObservableProperty]
    private bool _signUpFormVisible;
    [ObservableProperty]
    private bool _loginFormVisible;
    [ObservableProperty]
    private bool _isErrorVisible;
    [ObservableProperty]
    private bool _isResultVisible;

    private IAPIHelper _apiHelper;
    private readonly IMessenger _messenger;

    public LoginViewModel(IAPIHelper apiHelper, IMessenger messenger)
    {
        IsErrorVisible = false;

        LoginFormVisible = true;
        SignUpFormVisible = false;

        Title = "LOGIN";

        _apiHelper = apiHelper;
        _messenger = messenger;
    }

    public UserDisplayModel User { get; set; }

    [RelayCommand]
    private void SwitchForms()
    {
        LoginFormVisible = !LoginFormVisible;
        SignUpFormVisible = !SignUpFormVisible;

        IsResultVisible = false;

        Username = string.Empty;
        Password = string.Empty;
        Email = string.Empty;

        if (Title == "LOGIN")
            Title = "REGISTER";
        else
            Title = "LOGIN";
    }

    [RelayCommand]
    private async Task GoToMainView()
    {
        try
        {
            IsBusy = true;
            LoginFormVisible = false;

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                throw new Exception("Please don't leave any fields blank");
            else
                await Auth();

            await Shell.Current.GoToAsync(nameof(MainView), true, new Dictionary<string, object>
            {
                { "User", User }
            });
        }
        catch (WebException)
        {
            ErrorMessage = "Trouble connecting to server";

            IsErrorVisible = true;
        }
        catch (Exception ex)
        {
            if (ex.Message == "Unauthorized")
                ErrorMessage = "Wrong Email or Password";
            else
                ErrorMessage = "Something went wrong";

            IsErrorVisible = true;
        }
        finally
        {
            IsBusy = false;
            LoginFormVisible = true;
        }
        
    }

    [RelayCommand]
    private async void Register()
    {
        try
        {
            IsResultVisible = false;

            await _apiHelper.Register(Username, Password, Email);
        }
        catch(Exception ex)
        {
            ResultMessage = ex.Message.Trim('"');
        }
        finally
        {
            IsResultVisible = true;
        }
        
    }

    private async Task Auth()
    {
        var result = await _apiHelper.Authenticate(Email, Password);

        await _apiHelper.GetUserInfo(result.Token);

        User = new UserDisplayModel
        {
            Id = _apiHelper.LoggedInUser.Id,
            Username = _apiHelper.LoggedInUser.Username,
            Email = _apiHelper.LoggedInUser.Email
        };

        Preferences.Set("UserPassword", Password);

        IsErrorVisible = false;
        IsResultVisible = false;
        Password = string.Empty;
        Email = string.Empty;
    }
}

