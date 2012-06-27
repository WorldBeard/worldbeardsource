using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;

namespace WorldBeardEngine.Util
{
    public class PositionUtil
    {

        public static double GetScreenX(double worldX)
        {
            if (SingletonFactory.GetCamera() == null) { return worldX; }
            return worldX + SingletonFactory.GetCamera().XOffset;
        }

        public static double GetScreenY(double worldY)
        {
            if (SingletonFactory.GetCamera() == null) { return worldY; }
            return worldY + SingletonFactory.GetCamera().YOffset;
        }

        public static Vector GetWorldPosition(Point screenPosition)
        {
            return GetWorldPosition(new Vector(screenPosition));
        }

        public static Vector GetWorldPosition(Vector screenPosition)
        {
            if (SingletonFactory.GetCamera() == null) { return screenPosition; }
            Vector adjustedPosition = screenPosition - SingletonFactory.GetCamera().Offset;
            return adjustedPosition;
        }

    }
}
