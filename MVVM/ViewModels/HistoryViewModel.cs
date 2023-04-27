using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FictionMobile.MVVM.Models;
using FictionMobile.MVVM.Views;
using FictionMobile.Resources.Messangers;
using Maui_UI_Fiction_Library.API;
using System.Collections.ObjectModel;

namespace FictionMobile.MVVM.ViewModels;

[QueryProperty(nameof(User), "User")]
public partial class HistoryViewModel : BaseViewModel, IRecipient<AddToHistoryMessage>
{
    [ObservableProperty]
    private UserDisplayModel _user;
    [ObservableProperty]
    private ObservableCollection<StoryDisplayModel> _storiesRead;
    private readonly IMapper _mapper;
    private readonly IMessenger _messenger;
    private readonly IStoryEndpoint _storyEndpoint;

    public HistoryViewModel(IMapper mapper, IMessenger messenger, IStoryEndpoint storyEndpoint)
    {
        _mapper = mapper;
        _messenger = messenger;
        _storyEndpoint = storyEndpoint;

        _messenger.Register<AddToHistoryMessage>(this);
    }

    [RelayCommand]
    private async void GoToRead(StoryDisplayModel story)
    {
        if (story is null)
            return;

        await _storyEndpoint.AddToStoryHistory(story.Id);

        StoriesRead.Remove(story);
        StoriesRead.Insert(0, story);

        await Shell.Current.GoToAsync(nameof(ReadingView), true, new Dictionary<string, object>
        {
            { "SelectedStory", story }
        });
    }

    private async void FillHistory()
    {
        var payload = await _storyEndpoint.GetUserStoryHistory();

        var stories = _mapper.Map<IEnumerable<StoryDisplayModel>>(payload);

        StoriesRead = new ObservableCollection<StoryDisplayModel>(stories);
    }

    partial void OnUserChanged(UserDisplayModel value)
    {
        FillHistory();
    }

    public void Receive(AddToHistoryMessage message)
    {
        //Check if story already exists in history list
        var story = StoriesRead.SingleOrDefault(s => s.Id == message.Value.Id);

        if(story == null)
        {
            //If story doesn't exist in list -> create new story object to add to list
            story = new StoryDisplayModel
            {
                Id = message.Value.Id,
                Title = message.Value.Title,
                Author = message.Value.Author,
                Chapters = message.Value.Chapters,
                Summary = message.Value.Summary,
                EpubFile = message.Value.EpubFile
            };
        }
        else
        {
            //if story already exists -> remove it from it's current position
            StoriesRead.Remove(story);
        }

        //Move story to the top of history list
        StoriesRead.Insert(0, story);
    }
}

