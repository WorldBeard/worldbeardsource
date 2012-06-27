using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine.Services.Rendering.Cameras
{
    public class StaticPositionableCamera : Camera
    {

        private double _xPosition;
        private double _yPosition;

        private bool _isWaitingOneFrameBeforeClearing = false;

        protected override double GetXOffset()
        {
            return -_xPosition;
        }

        protected override void SetXOffset(double value)
        {
            _xPosition = -value;
            _isWaitingOneFrameBeforeClearing = true;
        }

        protected override double GetYOffset()
        {
            return -_yPosition;
        }

        protected override void SetYOffset(double value)
        {
            _yPosition = -value;
            _isWaitingOneFrameBeforeClearing = true;
        }

        public override void Update()
        {
            if (_isWaitingOneFrameBeforeClearing)
            {
                _isWaitingOneFrameBeforeClearing = false;
                return;
            }
            if (_xPosition != 0) { _xPosition = 0; }
            if (_yPosition != 0) { _yPosition = 0; }
        }

    }
}
