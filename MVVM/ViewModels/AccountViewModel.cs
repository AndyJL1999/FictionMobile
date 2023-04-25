using CommunityToolkit.Mvvm.Input;
using FictionMobile.MVVM.Views;
using Maui_UI_Fiction_Library.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.MVVM.ViewModels;

public partial class AccountViewModel : BaseViewModel
{
    private readonly IAPIHelper _apiHelper;

    public AccountViewModel(IAPIHelper apiHelper)
    {
        _apiHelper = apiHelper;
    }

    [RelayCommand]
    private async Task LogOut()
    {
        _apiHelper.ClearLoggedInUser();
        await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
    }
}

