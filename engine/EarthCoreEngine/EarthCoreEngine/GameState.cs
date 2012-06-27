using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthCoreEngine
{
    public abstract class GameState
    {
        public void OnLoad() { }
        public void OnUnload() { }

        public abstract void Update(double elapsedTime);
        public abstract void Render();
    }
}
