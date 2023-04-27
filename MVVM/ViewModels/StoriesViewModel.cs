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
public partial class StoriesViewModel : BaseViewModel, IRecipient<AddToStoriesMessenger>
{
    [ObservableProperty]
    private UserDisplayModel _user;
    [ObservableProperty]
    private ObservableCollection<StoryDisplayModel> _stories;
    private readonly IMapper _mapper;
    private readonly IMessenger _messenger;
    private readonly IStoryEndpoint _storyEndpoint;

    public StoriesViewModel(IMapper mapper, IMessenger messenger, IStoryEndpoint storyEndpoint)
    {
        _mapper = mapper;
        _messenger = messenger;
        _storyEndpoint = storyEndpoint;

        _messenger.Register<AddToStoriesMessenger>(this);
    }

    [RelayCommand]
    private async void GoToRead(StoryDisplayModel story)
    {
        if (story is null)
            return;

        await _storyEndpoint.AddToStoryHistory(story.Id);

        _messenger.Send(new AddToHistoryMessage(story));

        await Shell.Current.GoToAsync(nameof(ReadingView), true, new Dictionary<string, object>
        {
            { "SelectedStory", story }
        });
    }

    private async void FillStories()
    {
        IsBusy = true;

        var payload = await _storyEndpoint.GetUserStories();

        var stories = _mapper.Map<IEnumerable<StoryDisplayModel>>(payload);

        Stories = new ObservableCollection<StoryDisplayModel>(stories);

        IsBusy = false;
    }

    partial void OnUserChanged(UserDisplayModel value)
    {
        //When a new user is logged in get their specific stories
        FillStories();
    }

    public void Receive(AddToStoriesMessenger message)
    {
        //When a new story is added refresh Stories collection
        FillStories();
    }
}
