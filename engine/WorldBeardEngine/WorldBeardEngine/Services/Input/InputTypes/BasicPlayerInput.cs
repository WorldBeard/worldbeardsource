using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;
using WorldBeardEngine.Events;
using WorldBeardEngine.Services.Input.Controls;

namespace WorldBeardEngine.Services.Input.InputTypes
{
    class BasicPlayerInput : InputComponent
    {
        private bool wasMovingLastFrame = false;

        public BasicPlayerInput(String subclass) : base(subclass) { }

        public override void Update(double elapsedTime)
        {
            int xMove = 0;
            int yMove = 0;

            if (ControlMapping.IsKeyHeld(InputCommand.MoveLeft))
            {
                xMove = -1;
            }
            else if (ControlMapping.IsKeyHeld(InputCommand.MoveRight))
            {
                xMove = 1;
            }

            if (ControlMapping.IsKeyHeld(InputCommand.MoveUp))
            {
                yMove = 1;
            }
            else if (ControlMapping.IsKeyHeld(InputCommand.MoveDown))
            {
                yMove = -1;
            }
            if (xMove != 0 || yMove != 0)
            {
                wasMovingLastFrame = true;
                Vector direction = Vector.Normalize(new Vector(xMove, yMove));
                SingletonFactory.GetEventHub().SendEvent("MoveCommand", this, EventArgWriteUtil.GetMoveCommandArgs(direction, ConfigSettings.PLAYER_SPEED), _parent);
            }
            else if (wasMovingLastFrame)
            {
                wasMovingLastFrame = false;
                SingletonFactory.GetEventHub().SendEvent("MoveCommand", this, EventArgWriteUtil.GetMoveCommandArgs(new Vector(0, 0), 0), _parent);
            }
        }

        public override void Dispose()
        {
            // INTENTIONALLY BLANK
        }

    }
}
