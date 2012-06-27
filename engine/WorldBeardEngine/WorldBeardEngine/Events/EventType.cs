using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine.Events
{
    public enum EventType
    {
        NULL,
        ChangeTextureCommand,
        Colliding,
        Damaged,
        MoveCommand,
        OnClick,
        SetPositionCommand
    }
}
