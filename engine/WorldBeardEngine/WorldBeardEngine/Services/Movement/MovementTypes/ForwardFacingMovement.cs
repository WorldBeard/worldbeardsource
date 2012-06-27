using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Movement.MovementTypes
{
    class ForwardFacingMovement : BasicMovement
    {
        private Vector _previousDirection;

        public ForwardFacingMovement(String subclass, Vector direction, double speed)
            : base(subclass, direction, speed)
        {
            _previousDirection = new Vector(direction.X, direction.Y);
        }

        public override void Move(double elapsedTime)
        {
            //_parent.GetModel().Rotation += elapsedTime * (2 * Math.PI / 3);
            if (ChangedDirection())
            {
                _parent.Model.Rotation = _direction.Angle2D - Math.PI / 2;
                _previousDirection.X = _direction.X;
                _previousDirection.Y = _direction.Y;
            }
            base.Move(elapsedTime);
        }

        private bool ChangedDirection()
        {
            if (_previousDirection == _direction || _direction == Vector.Zero)
            {
                return false;
            }
            return true;
        }

    }
}
