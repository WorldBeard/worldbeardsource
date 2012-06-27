using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine.Components
{
    public class ComponentTypeUtil
    {

        public static ComponentType GetTypeFromString(string type)
        {
            switch (type.ToUpper())
            {
                case "ANIMATION":
                    return ComponentType.Animation;
                case "COLLISION":
                    return ComponentType.Collision;
                case "GUI":
                    return ComponentType.GUI;
                case "INPUT":
                    return ComponentType.Input;
                case "MOVEMENT":
                    return ComponentType.Movement;
                case "PHYSICS":
                    return ComponentType.Physics;
                case "RENDERING":
                    return ComponentType.Rendering;
                default:
                    throw new Exception("Invalid string in ComponentTypeUtil: " + type);
            }
        }

        public static string GetStringFromType(ComponentType type)
        {
            switch (type)
            {
                case ComponentType.Animation:
                    return "Animation";
                case ComponentType.Collision:
                    return "Collision";
                case ComponentType.GUI:
                    return "GUI";
                case ComponentType.Input:
                    return "Input";
                case ComponentType.Movement:
                    return "Movement";
                case ComponentType.Physics:
                    return "Physics";
                case ComponentType.Rendering:
                    return "Rendering";
                default:
                    throw new Exception("Invalid component type in ComponentTypeUtil: " + type);
            }
        }

        public static string GetCodeFromType(ComponentType type)
        {
            switch (type)
            {
                case ComponentType.Animation:
                    return "ANI";
                case ComponentType.Collision:
                    return "COL";
                case ComponentType.GUI:
                    return "GUI";
                case ComponentType.Input:
                    return "INP";
                case ComponentType.Movement:
                    return "MOV";
                case ComponentType.Physics:
                    return "PHY";
                case ComponentType.Rendering:
                    return "REN";
                default:
                    throw new Exception("Invalid component type in ComponentTypeUtil: " + type);
            }
        }

    }
}
