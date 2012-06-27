using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthCoreEngine
{
    public class FormData
    {
        private double _width;
        public double Width { get { return _width; } set { _width = value; } }
        public double HalfWidth { get { return _width / 2; } set { _width = 2 * value; } }

        private double _height;
        public double Height { get { return _height; } set { _height = value; } }
        public double HalfHeight { get { return _height / 2; } set { _height = 2 * value; } }

        public double Left { get { return -0.5 * _width; } }
        public double Right { get { return 0.5 * _width; } }
        public double Top { get { return 0.5 * _height; } }
        public double Bottom { get { return -0.5 * _height; } }

        public FormData(double width, double height)
        {
            _width = width;
            _height = height;
        }

    }
}
