using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Events;

namespace WorldBeardEngine.Load
{
    public struct GameEventDO
    {
        public String Name;
        public EventType Type;
        public Dictionary<string, Object> EventArgs;
    }
}
