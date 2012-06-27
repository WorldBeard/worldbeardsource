using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Rendering
{
    public class RenderingComponent : Component
    {

        protected Sprite _model;
        public Sprite Model { get { return _model; } set { _model = value; } }

        public RenderingComponent(string subclass, Sprite model)
            : base(subclass, ComponentType.Rendering)
        {
            _model = model;
        }

        public override void Dispose()
        {
            _model = null;
        }

    }
}
