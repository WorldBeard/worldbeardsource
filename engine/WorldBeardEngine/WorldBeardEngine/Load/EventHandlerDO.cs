using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Events;
using WorldBeardEngine.Components;

namespace WorldBeardEngine.Load
{
    public struct EventHandlerDO
    {
        public String Name;
        public EventType TypeToHandle;
        public List<GameEventDO> ThrownEvents;
        public Dictionary<string, string> HandlerArgs;
    }
}
