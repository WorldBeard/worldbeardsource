using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Collision
{
    public abstract class CollisionComponent : Component
    {
        protected Sprite _model { get { return _parent.Model; } }
        public Vector Center { get { return _model.Center; } }

        public CollisionComponent(String subclass) : base(subclass, ComponentType.Collision) { }

        public override void Dispose()
        {
            // INTENTIONALLY BLANK
        }

    }
}
