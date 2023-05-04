using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using EpubSharp;
using FictionMobile.MVVM.Models;
using Maui_UI_Fiction_Library.API;
using System.Collections.ObjectModel;
using FictionMobile.MVVM.Views.PopUps;

namespace FictionMobile.MVVM.ViewModels;

[QueryProperty(nameof(StoryInfo), "SelectedStory")]
public partial class ReadingViewModel : BaseViewModel
{
    [ObservableProperty]
    private StoryDisplayModel _storyInfo;
    [ObservableProperty]
    private ObservableCollection<ObservableCollection<string>> _chapterList;
    [ObservableProperty]
    private ObservableCollection<string> _fonts;
    [ObservableProperty]
    private ObservableCollection<int> _fontSizes;

    private EpubBook _book;

    private readonly IStoryEndpoint _storyEndpoint;

    //TODO - Finish setting up fonts and font sizes for page
    public ReadingViewModel(IStoryEndpoint storyEndpoint)
    {
        _storyEndpoint = storyEndpoint;

        Fonts = new ObservableCollection<string>
        {
            "Arial",
            "Verdana",
            "Tahoma",
            "Trebuchet MS",
            "Times New Roman",
            "Georgia",
            "Garamond",
            "Courier New",
            "Brush Script MT"
        };

        FontSizes = new ObservableCollection<int>
        {
            8, 12, 16, 18, 21, 24, 32, 48, 64
        };

    }

    [RelayCommand]
    public void SetFont()
    {
        var popup = new FontPopUp(this);

        Shell.Current.ShowPopup(popup);
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
        var pages = new ObservableCollection<ObservableCollection<string>>();

        foreach(var chapter in chapters)
        {
            chapter.TextContent = SetChapterStyle(chapter.TextContent ,"#272537", "AliceBlue", "Ariel");
            pages.Add(new ObservableCollection<string> { chapter.TextContent });
        }

        ChapterList = pages;
    }

    private string SetChapterStyle(string pageText, string backgroundColor, string fontColor, string fontFamily)
    {
        //Adding background color and font styles to <body> tag of html
        int index = pageText.IndexOf("<body>");
        pageText = pageText.Insert(index + 5, $" bgcolor=\"{backgroundColor}\" style=\"color:{fontColor}; font-family:{fontFamily}\" ");

        return pageText;
    }

    partial void OnStoryInfoChanged(StoryDisplayModel value)
    {
        if(ChapterList != null)
            ChapterList.Clear();

        SetPage();
    }

}
