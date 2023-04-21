using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FictionMobile.MVVM.Views;
using Maui_UI_Fiction_Library.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.MVVM.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _username;
        [ObservableProperty]
        private string _email;
        [ObservableProperty]
        private string _password;
        [ObservableProperty]
        private bool _signUpFormVisible;
        [ObservableProperty]
        private bool _loginFormVisible;

        private IAPIHelper _apiHelper;

        //TODO - Wire up Login and Sign up functions

        public LoginViewModel(IAPIHelper apiHelper)
        {
            LoginFormVisible = true;
            SignUpFormVisible = false;

            Title = "LOGIN";

            _apiHelper = apiHelper;
        }

        [RelayCommand]
        private void SwitchForms()
        {
            LoginFormVisible = !LoginFormVisible;
            SignUpFormVisible = !SignUpFormVisible;

            if (Title == "LOGIN")
                Title = "REGISTER";
            else
                Title = "LOGIN";
        }

        [RelayCommand]
        private async Task GoToMainView()
        {
            IsBusy = true;

            var result = await _apiHelper.Authenticate(Email, Password);

            await _apiHelper.GetUserInfo(result.Token);

            await Shell.Current.GoToAsync(nameof(MainView));

            IsBusy = false;
        }
    }
}
