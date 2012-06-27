using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Sdl;

namespace EarthCoreEngine.Input
{
    public class XboxController : IDisposable
    {
        IntPtr _joystick;

        // Joysticks
        public ControllerStick LeftControlStick { get; private set; }
        public ControllerStick RightControlStick { get; private set; }

        // BUTTONS
        // Main buttons
        public ControllerButton ButtonA { get; private set; }
        public ControllerButton ButtonB { get; private set; }
        public ControllerButton ButtonX { get; private set; }
        public ControllerButton ButtonY { get; private set; }

        // Front shoulder buttons
        public ControllerButton ButtonLB { get; private set; }
        public ControllerButton ButtonRB { get; private set; }

        // Back and start buttons
        public ControllerButton ButtonBack { get; private set; }
        public ControllerButton ButtonStart { get; private set; }

        // Pressing the controller stick in
        public ControllerButton ButtonL3 { get; private set; }
        public ControllerButton ButtonR3 { get; private set; }


        // Triggers
        public ControllerTrigger LeftTrigger { get; private set; }
        public ControllerTrigger RightTrigger { get; private set; }

        // DPad
        public ControllerDPad Dpad { get; private set; }


        public XboxController(int player)
        {
            _joystick = Sdl.SDL_JoystickOpen(player);

            LeftControlStick = new ControllerStick(_joystick, 0, 1);
            RightControlStick = new ControllerStick(_joystick, 4, 3);

            ButtonA = new ControllerButton(_joystick, 0);
            ButtonB = new ControllerButton(_joystick, 1);
            ButtonX = new ControllerButton(_joystick, 2);
            ButtonY = new ControllerButton(_joystick, 3);

            ButtonLB = new ControllerButton(_joystick, 4);
            ButtonRB = new ControllerButton(_joystick, 5);

            ButtonBack = new ControllerButton(_joystick, 6);
            ButtonStart = new ControllerButton(_joystick, 7);

            ButtonL3 = new ControllerButton(_joystick, 8);
            ButtonR3 = new ControllerButton(_joystick, 9);

            LeftTrigger = new ControllerTrigger(_joystick, 2, false);
            RightTrigger = new ControllerTrigger(_joystick, 2, true);

            Dpad = new ControllerDPad(_joystick, 0);
        }


        public void Update()
        {
            LeftControlStick.Update();
            RightControlStick.Update();

            ButtonA.Update();
            ButtonB.Update();
            ButtonX.Update();
            ButtonY.Update();

            ButtonLB.Update();
            ButtonRB.Update();

            ButtonBack.Update();
            ButtonStart.Update();

            ButtonL3.Update();
            ButtonR3.Update();

            LeftTrigger.Update();
            RightTrigger.Update();

            Dpad.Update();
        }


        public void Dispose()
        {
            Sdl.SDL_JoystickClose(_joystick);
        }

    }
}
