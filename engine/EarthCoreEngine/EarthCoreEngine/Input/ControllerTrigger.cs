﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Sdl;

namespace EarthCoreEngine.Input
{
    public class ControllerTrigger
    {
        IntPtr _joystick;
        int _index;
        bool _top = false; // The triggers are treated as axes and need to be split up
        float _deadZone = 0.24f;

        public float Value { get; private set; }


        public ControllerTrigger(IntPtr joystick, int index, bool top)
        {
            _joystick = joystick;
            _index = index;
            _top = top;
        }


        public void Update()
        {
            Value = MapZeroToOne(Sdl.SDL_JoystickGetAxis(_joystick, _index));
        }


        private float MapZeroToOne(short value)
        {
            float output = ((float)value / short.MaxValue);

            if (_top == false)
            {
                if (output > 0)
                {
                    output = 0;
                }
                output = Math.Abs(output);
            }

            // Avoid rounding error
            output = Math.Min(output, 1.0f);
            output = Math.Max(output, 0.0f);

            if (Math.Abs(output) < _deadZone)
            {
                output = 0;
            }

            return output;
        }

    }
}
