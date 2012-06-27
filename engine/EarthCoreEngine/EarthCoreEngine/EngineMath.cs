using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthCoreEngine
{
    public class EngineMath
    {

        public static double Distance(Point p1, Point p2)
        {
            double XDiff = p1.X - p2.X;
            double YDiff = p1.Y - p2.Y;

            double length = System.Math.Sqrt(XDiff * XDiff + YDiff * YDiff);

            return length;
        }

        public static double Distance(Vector v1, Vector v2)
        {
            double XDiff = v1.X - v2.X;
            double YDiff = v1.Y - v2.Y;

            double length = System.Math.Sqrt(XDiff * XDiff + YDiff * YDiff);

            return length;
        }

    }
}
