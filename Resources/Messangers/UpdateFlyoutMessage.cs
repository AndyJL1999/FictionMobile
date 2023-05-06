using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.Resources.Messangers
{
    public class UpdateFlyoutMessage : ValueChangedMessage<ObservableCollection<string>>
    {
        public UpdateFlyoutMessage(ObservableCollection<string> value) : base(value)
        {
        }
    }
}
