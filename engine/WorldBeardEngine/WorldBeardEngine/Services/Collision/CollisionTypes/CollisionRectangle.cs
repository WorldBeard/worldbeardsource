using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Collision.CollisionTypes
{
    class CollisionRectangle : CollisionComponent
    {
        private double _collisionPercent;
        public double CollisionPercent { get { return _collisionPercent; } }

        public double Width { get { return _model.Width; } }
        public double Height { get { return _model.Height; } }
        public Vector TopLeft { get { return _model.TopLeft; } }
        public Vector TopRight { get { return _model.TopRight; } }
        public Vector BottomLeft { get { return _model.BottomLeft; } }
        public Vector BottomRight { get { return _model.BottomRight; } }
        public double Rotation { get { return _model.Rotation; } }

        public CollisionRectangle(String subclass) : this(subclass, 1) { }

        public CollisionRectangle(String subclass, double collisionPercent)
            : base(subclass)
        {
            if (collisionPercent <= 0 || collisionPercent > 1) { throw new Exception(collisionPercent + " is not a valid CollisionRectangle collision percentage."); }
            _collisionPercent = collisionPercent;
        }

        public override string ToString()
        {
            return "CollisionRect/Percent" + _collisionPercent;
        }

    }
}
