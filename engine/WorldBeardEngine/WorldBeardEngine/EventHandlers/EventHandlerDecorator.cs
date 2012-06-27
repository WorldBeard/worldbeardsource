using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.EventHandlers;
using WorldBeardEngine.Events;
using WorldBeardEngine.Load;

namespace WorldBeardEngine.EventHandlers
{
    public class EventHandlerDecorator
    {
        private HandlerDelegateFactory _eventHandlerFactory = new HandlerDelegateFactory();
        private EventHandlerArgWriteUtil _eventHandlerArgWriteUtil = new EventHandlerArgWriteUtil();

        public void AddHandler(Component component, string handlerName, EventType typeToHandle, Dictionary<string, string> handlerArgs)
        {
            GameEventHandler gameEventHandler = new GameEventHandler(component, typeToHandle);
            gameEventHandler.SetDelegate(_eventHandlerFactory.GetDelegate(handlerName));
            // TODO - handlerArgsWriteUtil inside SetHandlerArgs
            if (handlerArgs != null && handlerArgs.Count > 0) { gameEventHandler.SetHandlerArgs(_eventHandlerArgWriteUtil.GetHandlerArgsFromDO(handlerArgs)); }
            component.RegisterEventHandler(gameEventHandler);
        }

    }
}
