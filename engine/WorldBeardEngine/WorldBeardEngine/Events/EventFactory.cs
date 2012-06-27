using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine.Events
{
    public class EventFactory
    {
        public GameEvent GetGameEvent(string eventType) { return GetGameEvent(eventType, null); }

        public GameEvent GetGameEvent(string eventType, Object source) { return GetGameEvent(eventType, source, new Dictionary<string, Object>()); }

        public GameEvent GetGameEvent(string eventType, Object source, Dictionary<string, Object> eventArgs)
        {
            GameEvent gameEvent = new GameEvent(EventTypeUtil.GetTypeFromString(eventType));
            gameEvent.Source = source;
            if (eventArgs != null && eventArgs.Count > 0) { gameEvent.SetArgs(eventArgs); }
            return gameEvent;
        }
    }
}
