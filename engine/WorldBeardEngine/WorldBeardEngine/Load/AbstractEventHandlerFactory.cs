using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.EventHandlers;

namespace WorldBeardEngine.Load
{
    internal class AbstractEventHandlerFactory
    {
        internal void AddEventHandlerFromDO(Component component, EventHandlerDO eventHandlerDO)
        {
            // TODO - add handling for thrown events

            SingletonFactory.GetEventHandlerDecorator().AddHandler(component, eventHandlerDO.Name, eventHandlerDO.TypeToHandle, eventHandlerDO.HandlerArgs);
        }
    }
}
