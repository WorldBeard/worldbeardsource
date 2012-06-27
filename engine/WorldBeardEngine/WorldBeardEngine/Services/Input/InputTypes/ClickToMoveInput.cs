using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;
using EarthCoreEngine.Input;
using WorldBeardEngine.Events;
using WorldBeardEngine.Services.Input.Controls;
using Keys = System.Windows.Forms.Keys;

namespace WorldBeardEngine.Services.Input.InputTypes
{
    class ClickToMoveInput : InputComponent
    {

        private bool wasMovingLastFrame = false;

        public ClickToMoveInput(String subclass) : base(subclass) { }

        public override void Update(double elapsedTime)
        {
            if (_mouse.LeftHeld)
            {
                wasMovingLastFrame = true;
                Vector direction = Vector.Normalize(new Vector(_mouse.Position) - _parent.Model.Center);
                SingletonFactory.GetEventHub().SendEvent("MoveCommand", this, EventArgWriteUtil.GetMoveCommandArgs(direction, ConfigSettings.PLAYER_SPEED), _parent);
            }
            else if (wasMovingLastFrame)
            {
                wasMovingLastFrame = false;
                SingletonFactory.GetEventHub().SendEvent("MoveCommand", this, EventArgWriteUtil.GetMoveCommandArgs(new Vector(0, 0), 0), _parent);
            }
            else if (ControlMapping.IsKeyPressed(InputCommand.PositionRight))
            {
                Vector positionVector = new Vector(100, 0);
                SingletonFactory.GetEventHub().SendEvent("SetPositionCommand", this, EventArgWriteUtil.GetSetPositionArgs(positionVector), _parent);
            }
        }

        public override void Dispose()
        {
            // INTENTIONALLY BLANK
        }

    }
}
