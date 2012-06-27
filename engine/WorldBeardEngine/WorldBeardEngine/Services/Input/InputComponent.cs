using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using EarthCoreEngine.Input;

namespace WorldBeardEngine.Services.Input
{
    public abstract class InputComponent : Component
    {
        private EarthCoreEngine.Input.Input _input = SingletonFactory.GetInput();
        protected Keyboard _keyboard;
        protected Mouse _mouse;

        public InputComponent(String name)
            : base(name, ComponentType.Input)
        {
            _keyboard = _input.Keyboard;
            _mouse = _input.Mouse;
        }

        public abstract void Update(double elapsedTime);

    }
}
