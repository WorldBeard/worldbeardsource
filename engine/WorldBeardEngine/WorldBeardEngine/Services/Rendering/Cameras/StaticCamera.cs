using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine.Services.Rendering.Cameras
{
    public class StaticCamera : Camera
    {
        protected override double GetXOffset()
        {
            return 0;
        }

        protected override double GetYOffset()
        {
            return 0;
        }
    }
}
