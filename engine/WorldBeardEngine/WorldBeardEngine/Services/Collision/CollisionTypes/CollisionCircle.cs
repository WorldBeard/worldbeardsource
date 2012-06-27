using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Collision.CollisionTypes
{
    class CollisionCircle : CollisionComponent
    {
        private double _radius;
        public double Radius
        {
            get
            {
                return _radius == -1 ? _parent.Model.Width : _radius;
            }
        }

        public CollisionCircle(String subclass, double radius) : base(subclass)
        {
            if (radius < 0 && radius != -1) { throw new Exception(radius + " is not a valid CollisionCircle radius."); }
            _radius = radius;
        }

        public override string ToString()
        {
            return "CollisionCircle/Rad" + _radius;
        }
    }
}
