﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthCoreEngine
{
    public class FramesPerSecond
    {
        int _numberOfFrames = 0;
        double _timePassed = 0;

        public double CurrentFPS { get; set; }

        public void Process(double elapsedTime)
        {
            _numberOfFrames++;
            _timePassed += elapsedTime;

            if (_timePassed > 1)
            {
                CurrentFPS = (double)_numberOfFrames / _timePassed;
                _timePassed = 0;
                _numberOfFrames = 0;
            }
        }
    }
}