using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Rendering.Cameras
{
    public class SpriteFollowingCamera : Camera
    {
        private Sprite _targetSprite;

        private double _x;
        private double _y;

        private double TargetX { get { return _targetSprite.XPosition; } }
        private double TargetY { get { return _targetSprite.YPosition; } }

        public void SetTarget(Sprite targetSprite)
        {
            _targetSprite = targetSprite;
        }

        public override void Update()
        {
            _x = -TargetX;
            _y = -TargetY;
        }

        protected override double GetXOffset()
        {
            return _x;
        }

        protected override double GetYOffset()
        {
            return _y;
        }
    }
}
