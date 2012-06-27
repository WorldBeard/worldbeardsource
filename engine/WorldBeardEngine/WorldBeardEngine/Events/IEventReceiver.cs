using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine.Events
{
    public interface IEventReceiver
    {
        void OnEvent(GameEvent gameEvent);
    }
}
