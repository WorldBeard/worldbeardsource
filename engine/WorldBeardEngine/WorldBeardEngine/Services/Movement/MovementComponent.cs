using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Movement
{
    public abstract class MovementComponent : Component
    {
        protected Vector _direction;
        public Vector Direction { get { return _direction; } set { _direction = Vector.Normalize(value); } }

        protected double _speed;
        public double Speed { get { return _speed; } set { _speed = value; } }

        public double XVelocity { get { return _direction.X * _speed; } }
        public double YVelocity { get { return _direction.Y * _speed; } }

        public MovementComponent(string name, Vector direction, double speed)
            : base(name, ComponentType.Movement)
        {
            _direction = Vector.Normalize(direction);
            _speed = speed;
        }

        public abstract void Move(double elapsedTime);

        public override void Dispose()
        {
            // INTENTIONALLY BLANK
        }

    }
}
