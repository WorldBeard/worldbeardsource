using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;

namespace WorldBeardEngine.Load
{
    public struct ComponentDO
    {
        public ComponentType Type;
        public string Subclass;
        public Dictionary<String, String> Properties;
        public List<EventHandlerDO> GameEventHandlerDOs;
    }
}
