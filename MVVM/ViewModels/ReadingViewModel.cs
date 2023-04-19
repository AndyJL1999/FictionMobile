using CommunityToolkit.Mvvm.ComponentModel;
using EpubSharp;
using FictionMobile.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.MVVM.ViewModels
{
    [QueryProperty(nameof(StoryInfo), "SelectedStory")]
    public partial class ReadingViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _storyHtml;
        [ObservableProperty]
        private StoryDisplayModel _storyInfo;
        private EpubBook _book;

        public ReadingViewModel()
        {
          
        }

        private void SetPage(string backgroundColor, string fontColor, string fontFamily)
        {
            _book = EpubReader
                .Read(StoryInfo.EpubFile);

            var chapters = _book.Resources.Html;
            StoryHtml = chapters.ElementAt(1).TextContent;

            //Adding background color and font styles to <body> tag of html
            int index = StoryHtml.IndexOf("<body>");
            StoryHtml = StoryHtml.Insert(index + 5, $" bgcolor=\"{backgroundColor}\" style=\"color:{fontColor}; font-family:{fontFamily};\" ");
            
        }

    }
}
