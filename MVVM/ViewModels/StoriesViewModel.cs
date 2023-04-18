using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FictionMobile.MVVM.Models;
using FictionMobile.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.MVVM.ViewModels
{
    public partial class StoriesViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<StoryDisplayModel> _stories;
        [ObservableProperty]
        private StoryDisplayModel _selectedStory;

        //TODO - Make a reading page accessable from this page

        public StoriesViewModel()
        {
            Fill();
        }

        [RelayCommand]
        private async void GoToRead()
        {
            await Shell.Current.GoToAsync(nameof(ReadingView));
        }

        private void Fill()
        {
            Stories = new ObservableCollection<StoryDisplayModel>
            {
                new StoryDisplayModel
                {
                    Title = "White",
                    Author = "NeonZangetsu",
                    Chapters = "12",
                    Summary = "This is the summary.",
                    UserId = 1,
                    EpubFile = "",
                    Id = 12034
                }
            };
        }

    }
}
