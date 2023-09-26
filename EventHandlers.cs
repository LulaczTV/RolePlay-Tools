using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginAPI.Events;
using PluginAPI.Core.Attributes;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.EventArgs.Player;
using MEC;

namespace RolePlay_Tools
{
    public class EventHandlers
    {
#if !EXILED
        [PluginEvent]
        public void OnRoundEnd(RoundEndEvent ev)
#else
        public void OnRoundEnded(RoundEndedEventArgs ev)
#endif
        {
        }
    }
}
