using CommunityToolkit.Mvvm.Input;
using FictionMobile.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.MVVM.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {

        public MainViewModel()
        {

        }

        //TODO - Create GoTo commands for Acount, Library, and Search views

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

    }
}
