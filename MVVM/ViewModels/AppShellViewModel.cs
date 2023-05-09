using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FictionMobile.Resources.Messangers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.MVVM.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel, IRecipient<UpdateFlyoutMessage>
    {
        private readonly IMessenger _messenger;
        [ObservableProperty]
        private ObservableCollection<string> _chapterTitles;

        public AppShellViewModel(IMessenger messenger)
        {
            _messenger = messenger;

            ChapterTitles = new ObservableCollection<string>();

            _messenger.Register<UpdateFlyoutMessage>(this);
        }

        [RelayCommand]
        private void SelectChapter(string chapterTitle)
        {
            Shell.Current.FlyoutIsPresented = false;

            _messenger.Send(new SelectedChapterMessage(chapterTitle));
        }

        public void Receive(UpdateFlyoutMessage message)
        {
            ChapterTitles = new ObservableCollection<string>(message.Value);
        }
    }
}
