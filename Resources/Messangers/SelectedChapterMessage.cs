using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.Resources.Messangers
{
    public class SelectedChapterMessage : ValueChangedMessage<string>
    {
        public SelectedChapterMessage(string value) : base(value)
        {
        }
    }
}
