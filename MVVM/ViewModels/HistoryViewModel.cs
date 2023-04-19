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
    public partial class HistoryViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<StoryDisplayModel> _storiesRead;

        public HistoryViewModel()
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
            StoriesRead = new ObservableCollection<StoryDisplayModel>
            {
                new StoryDisplayModel
                {
                    Title = "Sir Artorias",
                    Author = "NeonZangetsu",
                    Chapters = "21",
                    Summary = "The abysswalker.",
                    UserId = 1,
                    EpubFile = "",
                    Id = 12035
                },
                new StoryDisplayModel
                {
                    Title = "Sir Ornstein",
                    Author = "NeonZangetsu",
                    Chapters = "24",
                    Summary = "The dragonslayer.",
                    UserId = 1,
                    EpubFile = "",
                    Id = 12036
                }
            };
        }
    }
}
