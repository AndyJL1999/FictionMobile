using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FictionMobile.MVVM.Views;
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

        //TODO - Wire up Login and Sign up functions

        public LoginViewModel()
        {
            LoginFormVisible = true;
            SignUpFormVisible = false;

            Title = "LOGIN";
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
            await Shell.Current.GoToAsync(nameof(MainView));
        }

    }
}
