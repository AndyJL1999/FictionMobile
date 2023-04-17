using CommunityToolkit.Mvvm.ComponentModel;
using FictionMobile.MVVM.Models;
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

        //TODO - Make a reading page accessable from this page

        public StoriesViewModel()
        {
            Fill();
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
