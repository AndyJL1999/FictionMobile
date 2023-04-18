using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersOne.Epub;

namespace FictionMobile.MVVM.ViewModels
{
    public partial class ReadingViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _storyHtml;
        private EpubBook _book;

        public ReadingViewModel()
        {
            SetPage("#272537", "white", "Arial");
        }

        private void SetPage(string backgroundColor, string fontColor, string fontFamily)
        {
            _book = EpubReader
                .ReadBook(@"C:\\Users\\andyl\\OneDrive\\Uploads\\Documents\\ePub files\\HP\\HP - DG\A Different Kind of Love - Xioni101.epub");

            var chapters = _book.Content.Html;
            StoryHtml = chapters.Values.ElementAt(1).Content;

            int index = StoryHtml.IndexOf("<body>");
            StoryHtml = StoryHtml.Insert(index + 5, $" bgcolor=\"{backgroundColor}\" style=\"color:{fontColor}; font-family:{fontFamily};\" ");
            
        }

    }
}
