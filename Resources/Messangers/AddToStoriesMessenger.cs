using CommunityToolkit.Mvvm.Messaging.Messages;
using FictionMobile.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.Resources.Messangers
{
    public class AddToStoriesMessenger : ValueChangedMessage<StoryDisplayModel>
    {
        public AddToStoriesMessenger(StoryDisplayModel story) : base(story)
        {

        }
    }
}
