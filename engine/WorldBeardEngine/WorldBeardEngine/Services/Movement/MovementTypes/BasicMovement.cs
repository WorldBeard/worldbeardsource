using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Movement.MovementTypes
{
    class BasicMovement : MovementComponent
    {

        public BasicMovement(String subclass, Vector direction, double speed)
            : base(subclass, direction, speed)
        {
        }

        public override void Move(double elapsedTime)
        {
            if (_parent.Model != null)
            {
                if (!(_direction.X == 0 && _direction.Y == 0) && _speed != 0)
                {
                    Vector movementVector = _direction * _speed * elapsedTime;
                    _parent.Model.Move(movementVector);
                }
            }
            else
            {
                throw new Exception("Attempting to move MovementComponent with no Parent.Model: " + FullName);
            }
        }

    }
}
