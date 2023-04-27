using CommunityToolkit.Mvvm.Messaging.Messages;
using FictionMobile.MVVM.Models;

namespace FictionMobile.Resources.Messangers
{
    public class AddToHistoryMessage : ValueChangedMessage<StoryDisplayModel>
    {
        public AddToHistoryMessage(StoryDisplayModel story) : base(story)
        {

        }
    }
}
