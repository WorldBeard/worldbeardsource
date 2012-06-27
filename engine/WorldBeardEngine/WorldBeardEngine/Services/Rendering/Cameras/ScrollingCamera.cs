using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Rendering.Cameras
{
    public class ScrollingCamera : Camera
    {
        private double _x;
        private double _y;

        private double _speed = ConfigSettings.PLAYER_SPEED / 2;
        private Vector _direction = Vector.Normalize(ConfigSettings.CAMERA_SCROLL_DIRECTION);

        public void SetSpeed(double speed)
        {
            _speed = speed;
        }

        public void SetDirection(Vector direction)
        {
            _direction = Vector.Normalize(direction);
        }

        public override void Update()
        {
            _x = _speed * _direction.X * GameClock.ElapsedTime;
            _y = _speed * _direction.Y * GameClock.ElapsedTime;
        }

        protected override double GetXOffset()
        {
            return -_x;
        }

        protected override double GetYOffset()
        {
            return -_y;
        }

    }
}
