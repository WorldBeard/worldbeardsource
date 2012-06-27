using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine.Services.GUI
{
    public class GuiTextureTypeUtil
    {

        public static GuiTextureType GetTypeFromString(string type)
        {
            switch (type.ToUpper())
            {
                case "NULL":
                    return GuiTextureType.NULL;
                case "MOUSE_OVER":
                    return GuiTextureType.MOUSE_OVER;
                case "MOUSE_DOWN":
                    return GuiTextureType.MOUSE_DOWN;
                default:
                    throw new Exception("Invalid event type in GuiTextureTypeUtil: " + type);
            }
        }

        public static string GetStringFromType(GuiTextureType type)
        {
            return type.ToString().ToUpper();
        }

    }
}
