using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;
using WorldBeardEngine.Components;
using WorldBeardEngine.EventHandlers;

namespace WorldBeardEngine.Services.GUI
{
    public class GuiComponent : Component
    {
        private bool _isClickable;
        public bool IsClickable { get { return _isClickable; } }

        public double Left { get { return _parent.Model.Left; } }
        public double Right { get { return _parent.Model.Right; } }
        public double Top { get { return _parent.Model.Top; } }
        public double Bottom { get { return _parent.Model.Bottom; } }

        public GuiComponent(string name, bool isClickable)
            : base(name, ComponentType.GUI)
        {
            _isClickable = isClickable;
        }

        public override void Dispose()
        {
            // INTENTIONALLY BLANK
        }

    }
}
