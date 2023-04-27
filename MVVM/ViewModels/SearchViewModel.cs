using Android.Telephony;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EpubSharp;
using FictionMobile.MVVM.Models;
using FictionMobile.Resources.Messangers;
using Java.Sql;
using Maui_UI_Fiction_Library.API;
using Maui_UI_Fiction_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace FictionMobile.MVVM.ViewModels;

public partial class SearchViewModel : BaseViewModel
{
    private EpubBook _book;
    [ObservableProperty]
    private StoryDisplayModel _story;
    [ObservableProperty]
    private string _resultMessage;
    [ObservableProperty]
    private Brush _resultColor;
    [ObservableProperty]
    private bool _isMessageVisible;
    [ObservableProperty]
    private bool _isSaveButtonVisible;
    private readonly IMapper _mapper;
    private readonly IMessenger _messenger;
    private readonly IStoryEndpoint _storyEndpoint;

    public SearchViewModel(IMapper mapper, IMessenger messenger, IStoryEndpoint storyEndpoint)
    {
        _mapper = mapper;
        _messenger = messenger;
        _storyEndpoint = storyEndpoint;

        Story = new StoryDisplayModel();
        IsSaveButtonVisible = false;
        IsMessageVisible = false;
    }

    [RelayCommand]
    private void ReturnToMain() //Clear fields when returning to MainView
    {
        Shell.Current.GoToAsync("..");
        ClearStoryInfo();
    }

    [RelayCommand]
    private async Task SearchForFile() //Search mobile storage for epub file
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "application/epub+zip" } }
                })

            });

            if (result != null)
            {
                if (result.FileName.EndsWith("epub", StringComparison.OrdinalIgnoreCase))
                {
                    var path = result.FullPath;

                    _book = EpubReader.Read(path);

                    if (_book != null)
                    {
                        Story.Title = _book.Title;
                        Story.Author = _book.Authors.FirstOrDefault();
                        Story.Chapters = _book.Resources.Html.Count.ToString();
                        Story.EpubFile = result.FullPath;

                        //Decode Html
                        string htmlToDecode = HttpUtility.HtmlDecode(_book.Resources.Html.ElementAt(0).TextContent);
                        //Get summary from html
                        var htmlGroups = Regex.Matches(htmlToDecode, @"</span>\s*(.+?)\s*<br />");
                        //Add summary to story model
                        if (htmlGroups.Count > 0)
                            Story.Summary = htmlGroups.ElementAt(1).Groups[1].Value;
                        else
                            Story.Summary = "No Summary";
                    }

                    IsSaveButtonVisible = true;
                }
            }

        }
        catch (Exception ex)
        {
            ResultMessage = "There was a problem";
        }
    }

    [RelayCommand]
    private async void AddStory() //Ensure story doesn't already exist, then adds it to library
    {
        try
        {
            IsBusy = true;

            var story = _mapper.Map<StoryModel>(Story);

            //Get stories for comparison
            var stories = await _storyEndpoint.GetUserStories();

            //checks if story already exists
            var comparer = stories.Where(s => (s.Title == Story.Title && s.Author == Story.Author)
                && (s.Chapters == Story.Chapters && s.Summary == Story.Summary)).FirstOrDefault();

            if (story != null && comparer is null)
            {
                await _storyEndpoint.InsertNewStory(story);

                _messenger.Send(new AddToStoriesMessenger(Story));

                ResultColor = new SolidColorBrush(Colors.LimeGreen);
                ResultMessage = "Upload Successful!";
                IsMessageVisible = true;
            }
            else
            {
                ResultColor = new SolidColorBrush(Colors.Red);
                ResultMessage = "Story may already exist in your library";
                IsMessageVisible = true;
            }

            IsSaveButtonVisible = false;
        }
        catch
        {
            ResultMessage = "Something went wrong";
            IsMessageVisible = true;
        }
        finally
        {
            IsBusy = false;
        }
        
    }

    private void ClearStoryInfo()
    {
        Story.Title = string.Empty;
        Story.Author = string.Empty;
        Story.Chapters = string.Empty;
        Story.Summary = string.Empty;
        Story.EpubFile = string.Empty;
        IsMessageVisible = false;
    }
}

