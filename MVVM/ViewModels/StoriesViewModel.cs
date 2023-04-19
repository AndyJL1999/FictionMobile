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

        public StoriesViewModel()
        {
            Fill();
        }

        [RelayCommand]
        private async void GoToRead(StoryDisplayModel story)
        {
            if (story is null)
                return;

            await Shell.Current.GoToAsync(nameof(ReadingView), true, new Dictionary<string, object>
            {
                { "SelectedStory", story }
            });
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
                },
                new StoryDisplayModel
                {
                    Title = "Harry Potter and the Conjoining Paragons",
                    Author = "J.K. Dumbdum",
                    Chapters = "8",
                    Summary = "This is the summary.",
                    UserId = 1,
                    EpubFile = "",
                    Id = 12024
                }
            };
        }

    }
}
