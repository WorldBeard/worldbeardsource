using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Sdl;

namespace EarthCoreEngine.Input
{
    public class ControllerStick
    {
        IntPtr _joystick;
        int _axisIdX = 0;
        int _axisIdY = 0;
        float _deadZone = 0.2f;


        public float X { get; private set; }
        public float Y { get; private set; }


        public ControllerStick(IntPtr joystick, int axisIdX, int axisIdY)
        {
            _joystick = joystick;
            _axisIdX = axisIdX;
            _axisIdY = axisIdY;
        }


        public void Update()
        {
            X = MapMinusOneToOne(Sdl.SDL_JoystickGetAxis(_joystick, _axisIdX));
            Y = MapMinusOneToOne(Sdl.SDL_JoystickGetAxis(_joystick, _axisIdY));
        }


        private float MapMinusOneToOne(short value)
        {
            float output = ((float)value / short.MaxValue);

            // Avoid rounding error
            output = Math.Min(output, 1.0f);
            output = Math.Max(output, -1.0f);

            if (Math.Abs(output) < _deadZone)
            {
                output = 0;
            }

            return output;
        }


    }
}
