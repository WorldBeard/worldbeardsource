using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine.Events.EventHub
{
    public abstract class EventFilter
    {
        private List<EventType> _typesToFilter;

        public EventFilter(EventType typeToFilter)
        {
            _typesToFilter = new List<EventType>();
            _typesToFilter.Add(typeToFilter);
        }

        public EventFilter(List<EventType> typesToFilter)
        {
            _typesToFilter = typesToFilter;
        }

        public void FilterEvent(GameEvent gameEvent)
        {
            if(_typesToFilter.Contains(gameEvent.Type))
            {
                DoFilter(gameEvent);
            }
        }

        protected abstract void DoFilter(GameEvent gameEvent);

    }
}
