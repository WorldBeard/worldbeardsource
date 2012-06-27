using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Movement.MovementTypes
{
    class CircleMovement : BasicMovement
    {
        private double _currentAngle = 2 * Math.PI;
        private int _radius;
        private int _timeToRotate;

        public CircleMovement(string subclass, Vector direction, double speed, int radius, int timeToRotate) : base(subclass, direction, speed)
        {
            _radius = radius;
            _timeToRotate = timeToRotate;
        }

        public override void Move(double elapsedTime)
        {
            _speed = 2 * Math.PI * _radius / _timeToRotate;
            _currentAngle += elapsedTime * 2 * Math.PI / _timeToRotate;
            if (_currentAngle > 2 * Math.PI) { _currentAngle -= 2 * Math.PI; }
            _direction = new Vector(-1 * Math.Sin(_currentAngle), Math.Cos(_currentAngle));
            base.Move(elapsedTime);
        }

    }
}
