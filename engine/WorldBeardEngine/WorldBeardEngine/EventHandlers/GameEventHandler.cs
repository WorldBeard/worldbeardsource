using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Events;

namespace WorldBeardEngine.EventHandlers
{
    public class GameEventHandler : IEventReceiver
    {
        private Component _parent;
        public Component Parent { set { _parent = value; _myType = value.Type; } }

        private ComponentType _myType = ComponentType.NULL;
        public ComponentType MyType { get { return _myType; } }

        private EventType _typeToHandle = EventType.NULL;
        public EventType TypeToHandle { get { return _typeToHandle; } }

        public delegate void HandleEvent(Component parent, GameEvent gameEvent, Dictionary<string, Object> handlerArgs);

        private Dictionary<string, Object> _handlerArgs = null;

        private HandleEvent DoMyHandleEvent;

        public GameEventHandler(Component parent, EventType typeToReactTo) : this(parent, typeToReactTo, null) { }

        public GameEventHandler(Component parent, EventType typeToReactTo, HandleEvent HandleEvent)
        {
            _parent = parent;
            _typeToHandle = typeToReactTo;
            DoMyHandleEvent = HandleEvent;
        }

        public void SetDelegate(HandleEvent handleEvent)
        {
            DoMyHandleEvent = handleEvent;
        }

        public void SetHandlerArg(string argName, string value)
        {
            if (_handlerArgs == null) { _handlerArgs = new Dictionary<string, Object>(); }
            _handlerArgs.Add(argName, value);
        }

        public void SetHandlerArgs(Dictionary<string, Object> handlerArgs)
        {
            _handlerArgs = handlerArgs;
        }

        public void OnEvent(GameEvent gameEvent)
        {
            if (gameEvent.Type == _typeToHandle)
            {
                DoMyHandleEvent(_parent, gameEvent, _handlerArgs);
            }
        }
    }
}
