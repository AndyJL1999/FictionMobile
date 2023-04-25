using Android.Graphics.Fonts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EpubSharp;
using FictionMobile.MVVM.Models;
using Maui_UI_Fiction_Library.API;
using System.Collections.ObjectModel;

namespace FictionMobile.MVVM.ViewModels;

[QueryProperty(nameof(StoryInfo), "SelectedStory")]
public partial class ReadingViewModel : BaseViewModel
{
    [ObservableProperty]
    private StoryDisplayModel _storyInfo;
    [ObservableProperty]
    private ObservableCollection<string> _chapterList;
    private EpubBook _book;
    private readonly IStoryEndpoint _storyEndpoint;

    public ReadingViewModel(IStoryEndpoint storyEndpoint)
    {
        _storyEndpoint = storyEndpoint;
    }

    private async void SetPage()
    {
        IsBusy = true;

        _book = EpubReader
            .Read(await _storyEndpoint.GetStoryForReading(StoryInfo.Id));

        var chapters = _book.Resources.Html;

        SetChapters(chapters);

        IsBusy = false;
    }

    private void SetChapters(ICollection<EpubTextFile> chapters)
    {
        var pages = new List<string>();

        foreach(var chapter in chapters)
        {
            chapter.TextContent = SetChapterStyle(chapter.TextContent ,"#272537", "AliceBlue", "Ariel");
            pages.Add(chapter.TextContent);
        }

        ChapterList = new ObservableCollection<string>(pages);
    }

    private string SetChapterStyle(string pageText, string backgroundColor, string fontColor, string fontFamily)
    {
        //Adding background color and font styles to <body> tag of html
        int index = pageText.IndexOf("<body>");
        pageText = pageText.Insert(index + 5, $" bgcolor=\"{backgroundColor}\" style=\"color:{fontColor}; font-family:{fontFamily}; overflow-y:scroll;\" ");

        return pageText;
    }

    partial void OnStoryInfoChanged(StoryDisplayModel value)
    {
        SetPage();
    }

}
