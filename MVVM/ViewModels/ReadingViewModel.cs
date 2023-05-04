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
    [ObservableProperty]
    private string currentFont;
    [ObservableProperty]
    private int currentFontSize;

    private EpubBook _book;

    private readonly IStoryEndpoint _storyEndpoint;

    //TODO - Finish setting up fonts and font sizes for page
    public ReadingViewModel(IStoryEndpoint storyEndpoint)
    {
        _storyEndpoint = storyEndpoint;

        Fonts = new ObservableCollection<string>
        {
            "Arial",
            "Times New Roman",
            "Courier New",
        };

        FontSizes = new ObservableCollection<int>
        {
            8, 12, 16, 18, 21, 24, 32
        };

        CurrentFont = "Arial";
        CurrentFontSize = 16;
    }

    [RelayCommand]
    public void OpenFontPopup()
    {
        var popup = new FontPopUp(this);

        Shell.Current.ShowPopup(popup);
    }

    [RelayCommand]
    public void ChangeFont(FontPopUp popUp)
    {
        foreach(var chapter in ChapterList)
        {
            chapter[0] = 
                SetChapterStyle(chapter[0], "#272537", "AliceBlue", CurrentFont, CurrentFontSize);
        }

        popUp.Close();
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

        chapters
            .ToList()
            .ForEach((c) => 
            { 
                c.TextContent = SetChapterStyle(c.TextContent, "#272537", "AliceBlue", CurrentFont, CurrentFontSize);
                pages.Add(new ObservableCollection<string> { c.TextContent });
            });

        ChapterList = pages;
    }

    private string SetChapterStyle(string pageText, string backgroundColor, string fontColor, string fontFamily, int fontSize)
    {
        //Adding background color and font styles to <body> tag of html
        int index = pageText.IndexOf("<body");

        if(pageText.ElementAt(index + 5) != '>')
        {
            var endIndex = pageText.IndexOf("px\"");
            var count = (endIndex + 3) - (index + 5);
            pageText = pageText.Remove(index + 5, count);
        }

        pageText = pageText.Insert(index + 5, $" bgcolor=\"{backgroundColor}\" style=\"color:{fontColor}; font-family:{fontFamily}; font-size:{fontSize}px\"");
        
        return pageText;
    }

    partial void OnStoryInfoChanged(StoryDisplayModel value)
    {
        if(ChapterList != null)
            ChapterList.Clear();

        SetPage();
    }

}
