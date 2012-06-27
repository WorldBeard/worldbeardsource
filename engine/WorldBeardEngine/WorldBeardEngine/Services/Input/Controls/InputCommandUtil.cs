using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine.Services.Input.Controls
{
    public class InputCommandUtil
    {

        public static InputCommand GetCommandFromString(string value)
        {
            switch (value.ToUpper())
            {
                case "EXIT":
                    return InputCommand.Exit;
                case "PAUSE":
                    return InputCommand.Pause;
                case "MOVEUP":
                    return InputCommand.MoveUp;
                case "MOVEDOWN":
                    return InputCommand.MoveDown;
                case "MOVELEFT":
                    return InputCommand.MoveLeft;
                case "MOVERIGHT":
                    return InputCommand.MoveRight;
                case "POSITIONUP":
                    return InputCommand.PositionUp;
                case "POSITIONDOWN":
                    return InputCommand.PositionDown;
                case "POSITIONLEFT":
                    return InputCommand.PositionLeft;
                case "POSITIONRIGHT":
                    return InputCommand.PositionRight;
                default:
                    throw new Exception("Value not recognized by InputCommandUtil: " + value);
            }
        }

    }
}
