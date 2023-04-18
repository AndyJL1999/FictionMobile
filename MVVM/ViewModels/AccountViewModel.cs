using CommunityToolkit.Mvvm.Input;
using FictionMobile.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.MVVM.ViewModels
{
    public partial class AccountViewModel : BaseViewModel
    {

        public AccountViewModel()
        {

        }

        [RelayCommand]
        private async Task LogOut()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
        }
    }
}
