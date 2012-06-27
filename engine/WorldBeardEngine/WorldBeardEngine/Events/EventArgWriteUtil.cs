using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;
using WorldBeardEngine.Services.GUI;

namespace WorldBeardEngine.Events
{
    public class EventArgWriteUtil
    {
        private static Dictionary<string, Object> args;

        // ChangeTextureCommand
        public static Dictionary<string, Object> GetChangeTextureCommandArgs(string textureName)
        {
            args = new Dictionary<string, Object>();
            args.Add("TextureName", textureName);
            return args;
        }
        public static Dictionary<string, Object> GetChangeTextureCommandArgs(GuiTextureType guiTextureType)
        {
            args = new Dictionary<string, Object>();
            args.Add("TextureName", GuiTextureTypeUtil.GetStringFromType(guiTextureType));
            return args;
        }

        // MoveCommand
        public static Dictionary<string, Object> GetMoveCommandArgs(Vector direction, double speed)
        {
            args = new Dictionary<string, Object>();
            args.Add("Direction", direction);
            args.Add("Speed", speed);
            return args;
        }

        // SetPosition
        public static Dictionary<string, Object> GetSetPositionArgs(Vector positionVector)
        {
            return GetSetPositionArgs(positionVector, true);
        }
        public static Dictionary<string, Object> GetSetPositionArgs(Vector positionVector, bool isRelative)
        {
            args = new Dictionary<string, Object>();
            args.Add("PositionVector", positionVector);
            args.Add("IsRelative", isRelative);
            return args;
        }

    }
}
