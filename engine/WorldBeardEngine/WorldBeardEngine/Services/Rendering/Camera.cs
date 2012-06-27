using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Rendering
{
    public abstract class Camera
    {
        public Vector Offset { get { return new Vector(GetXOffset(), GetYOffset()); } }
        public double XOffset
        {
            get { return GetXOffset(); }
            set { SetXOffset(value); }
        }
        public double YOffset
        {
            get { return GetYOffset(); }
            set { SetYOffset(value); }
        }

        protected abstract double GetXOffset();
        protected abstract double GetYOffset();

        protected virtual void SetXOffset(double value) { }
        protected virtual void SetYOffset(double value) { }

        public Boolean IsOnScreen(Sprite sprite)
        {
            double leftXShifted = sprite.Left + GetXOffset();
            double rightXShifted = sprite.Right + GetXOffset();

            double topYShifted = sprite.Top + GetYOffset();
            double bottomYShifted = sprite.Bottom + GetYOffset();
             
            double totalXBuffer = SingletonFactory.GetFormData().HalfWidth + ConfigSettings.CAMERA_BUFFER;
            double totalYBuffer = SingletonFactory.GetFormData().HalfHeight + ConfigSettings.CAMERA_BUFFER;

            bool onScreenX = Math.Abs(leftXShifted) < totalXBuffer || Math.Abs(rightXShifted) < totalXBuffer;
            bool onScreenY = Math.Abs(topYShifted) < totalYBuffer || Math.Abs(bottomYShifted) < totalYBuffer;

            if (onScreenX && onScreenY)
            {
                return true;
            }
            return false;
        }

        public virtual void Update()
        {
        }

    }
}
