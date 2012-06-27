using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Events;

namespace WorldBeardEngine.Events
{
    class EventTypeUtil
    {
        public static EventType GetTypeFromString(string type)
        {
            switch (type.ToUpper())
            {
                case "CHANGETEXTURECOMMAND":
                    return EventType.ChangeTextureCommand;
                case "COLLIDING":
                    return EventType.Colliding;
                case "DAMAGED":
                    return EventType.Damaged;
                case "MOVECOMMAND":
                    return EventType.MoveCommand;
                case "ONCLICK":
                    return EventType.OnClick;
                case "SETPOSITIONCOMMAND":
                    return EventType.SetPositionCommand;
                default:
                    throw new Exception("Invalid event type in EventTypeUtil: " + type);
            }
        }
    }
}
