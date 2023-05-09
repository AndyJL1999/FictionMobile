using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using EpubSharp;
using FictionMobile.MVVM.Models;
using Maui_UI_Fiction_Library.API;
using System.Collections.ObjectModel;
using FictionMobile.MVVM.Views.PopUps;
using CommunityToolkit.Mvvm.Messaging;
using FictionMobile.Resources.Messangers;
using FictionMobile.Interfaces;

namespace FictionMobile.MVVM.ViewModels;

[QueryProperty(nameof(StoryInfo), "SelectedStory")]
public partial class ReadingViewModel : BaseViewModel, IRecipient<SelectedChapterMessage>,
    IHasCollectionViewModel
{
    [ObservableProperty]
    private StoryDisplayModel _storyInfo;
    [ObservableProperty]
    private ObservableCollection<ObservableCollection<string>> _chapterList;
    [ObservableProperty]
    private ObservableCollection<string> _titles;
    [ObservableProperty]
    private ObservableCollection<string> _fonts;
    [ObservableProperty]
    private ObservableCollection<int> _fontSizes;
    [ObservableProperty]
    private string _currentFont;
    [ObservableProperty]
    private int _currentFontSize;
    [ObservableProperty]
    private int _selectedChapterIndex;
    [ObservableProperty]
    private bool _isUpVisible;
    [ObservableProperty]
    private bool _isDownVisible;

    private EpubBook _book;

    private readonly IStoryEndpoint _storyEndpoint;
    private readonly IMessenger _messenger;

    public IHasCollectionView View { get; set; }

    public ReadingViewModel(IStoryEndpoint storyEndpoint, IMessenger messenger)
    {
        _storyEndpoint = storyEndpoint;
        _messenger = messenger;

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

        _messenger.Register<SelectedChapterMessage>(this);
    }

    [RelayCommand]
    public void ScrollTop()
    {
        View.CollectionView.ScrollTo(0, -1, ScrollToPosition.Start, false);
    }

    [RelayCommand]
    public void ScrollBottom()
    {
        View.CollectionView.ScrollTo(1, -1, ScrollToPosition.End, true);
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

        var titleList = _book.TableOfContents
                .Select(c => c.Title)
                .ToList();

        Titles = new ObservableCollection<string>(titleList);

        _messenger.Send(new UpdateFlyoutMessage(Titles));

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

    public void Receive(SelectedChapterMessage message)
    {
        var diff = ChapterList.Count - Titles.Count;

        SelectedChapterIndex = Titles.IndexOf(
            Titles.Where(c => c == message.Value).FirstOrDefault());

        if (diff != 0)
        {
            View.CarouselView.ScrollTo(SelectedChapterIndex + 1, -1, ScrollToPosition.MakeVisible,false);
        }
        else
        {
            View.CarouselView.ScrollTo(SelectedChapterIndex, -1, ScrollToPosition.MakeVisible, false);
        }
    }
}
