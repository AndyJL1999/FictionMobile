using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FictionMobile.MVVM.Models;
using FictionMobile.MVVM.Views;
using Maui_UI_Fiction_Library.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.MVVM.ViewModels;

[QueryProperty(nameof(User), "User")]
public partial class HistoryViewModel : BaseViewModel
{
    [ObservableProperty]
    private UserDisplayModel _user;
    [ObservableProperty]
    private ObservableCollection<StoryDisplayModel> _storiesRead;
    private readonly IMapper _mapper;
    private readonly IStoryEndpoint _storyEndpoint;

    public HistoryViewModel(IMapper mapper, IStoryEndpoint storyEndpoint)
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
}

