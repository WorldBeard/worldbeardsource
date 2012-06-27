using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;

namespace WorldBeardEngine.Events.EventHub
{
    public class EventHub
    {
        private List<EventFilter> _filters = new List<EventFilter>();

        public void FireEvent(GameEvent gameEvent)
        {
            SingletonFactory.LOG.Log("FireEvent: " + gameEvent.ToString(), Logging.LoggerLevel.LOW);
            SingletonFactory.LOG.Log("EventArgs: " + gameEvent.ArgString);
            FilterEvent(gameEvent);
        }

        public void FireEvent(string eventType, object source)
        {
            FireEvent(SingletonFactory.GetEventFactory().GetGameEvent(eventType, source, null));
        }

        public void FireEvent(GameEvent gameEvent, object source)
        {
            gameEvent.Source = source;
            FireEvent(gameEvent);
        }

        public void SendEvent(GameEvent gameEvent, IEventReceiver receiver)
        {
            SingletonFactory.LOG.Log("SendEvent: " + gameEvent.ToString() + (receiver == null ? "" : ", Receiver: " + receiver.ToString()), Logging.LoggerLevel.LOW);
            SingletonFactory.LOG.Log("EventArgs: " + gameEvent.ArgString);
            FilterEvent(gameEvent);
            receiver.OnEvent(gameEvent);
        }

        public void SendEvent(GameEvent gameEvent, List<IEventReceiver> receivers)
        {
            foreach (IEventReceiver receiver in receivers)
            {
                SendEvent(gameEvent, receiver);
            }
        }

        public void SendEvent(GameEvent gameEvent, object source, IEventReceiver receiver)
        {
            gameEvent.Source = source;
            SendEvent(gameEvent, receiver);
        }

        public void SendEvent(string eventType, object source, IEventReceiver receiver)
        {
            SendEvent(eventType, source, null, receiver);
        }

        public void SendEvent(string eventType, object source, Dictionary<string, object> eventArgs, IEventReceiver receiver)
        {
            SendEvent(SingletonFactory.GetEventFactory().GetGameEvent(eventType, source, eventArgs), receiver);
        }

        private void FilterEvent(GameEvent gameEvent)
        {
            foreach (EventFilter filter in _filters)
            {
                filter.FilterEvent(gameEvent);
            }
        }

        public void AddFilter(EventFilter filter)
        {
            _filters.Add(filter);
        }

    }
}
