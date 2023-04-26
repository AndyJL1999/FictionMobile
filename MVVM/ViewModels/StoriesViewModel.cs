using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FictionMobile.MVVM.Models;
using FictionMobile.MVVM.Views;
using Maui_UI_Fiction_Library.API;
using Maui_UI_Fiction_Library.Models;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace FictionMobile.MVVM.ViewModels;

[QueryProperty(nameof(User), "User")]
public partial class StoriesViewModel : BaseViewModel
{
    [ObservableProperty]
    private UserDisplayModel _user;
    [ObservableProperty]
    private ObservableCollection<StoryDisplayModel> _stories;
    private readonly IMapper _mapper;
    private readonly IStoryEndpoint _storyEndpoint;

    public StoriesViewModel(IMapper mapper, IStoryEndpoint storyEndpoint)
    {
        _mapper = mapper;
        _storyEndpoint = storyEndpoint;
    }

    [RelayCommand]
    private async void GoToRead(StoryDisplayModel story)
    {
        if (story is null)
            return;

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
        FillStories();
    }

}
