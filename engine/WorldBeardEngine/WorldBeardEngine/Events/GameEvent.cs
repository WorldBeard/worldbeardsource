using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine.Events
{
    public class GameEvent
    {
        protected EventType _type;
        public EventType Type { get { return _type; } set { _type = value; } }

        protected Object _source;
        public Object Source { get { return _source; } set { _source = value; } }

        protected Dictionary<string, Object> _eventArgs;

        public GameEvent() : this(EventType.NULL, null) { }

        public GameEvent(EventType type) : this(type, null) { }

        public GameEvent(EventType type, Object source)
        {
            _type = type;
            _source = source;
        }

        public Object GetArg(string argName)
        {
            return _eventArgs[argName];
        }

        public void SetArg(string argName, Object value)
        {
            if (_eventArgs == null) { _eventArgs = new Dictionary<string, Object>(); }
            _eventArgs.Add(argName, value);
        }

        public void SetArgs(Dictionary<string, Object> eventArgs)
        {
            _eventArgs = eventArgs;
        }

        public override string ToString()
        {
            return "EventType: " + _type.ToString() + (_source == null ? ", No source" : ", Source: " + _source.ToString());
        }

        public string ArgString
        {
            get
            {
                string argString = "";
                if (_eventArgs != null && _eventArgs.Count > 0)
                {
                    foreach (string key in _eventArgs.Keys)
                    {
                        argString += key + ": " + _eventArgs[key].ToString() + "; ";
                    }
                }
                return argString;
            }
        }

    }
}
