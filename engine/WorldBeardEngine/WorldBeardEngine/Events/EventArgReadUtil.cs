using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Events;
using EarthCoreEngine;
using WorldBeardEngine.Services.GUI;

namespace WorldBeardEngine.Events
{
    public class EventArgReadUtil
    {
        // ChangeTextureCommand
        public static string GetTexture(GameEvent gameEvent)
        {
            if (gameEvent.Type != EventType.ChangeTextureCommand) { throw new Exception("EventArgUtil: Invalid gameEvent Type: " + gameEvent.Type + " in GetTargetTexture"); }
            return (string)gameEvent.GetArg("TextureName");
        }
        public static GuiTextureType GetGuiTextureType(GameEvent gameEvent)
        {
            if (gameEvent.Type != EventType.ChangeTextureCommand) { throw new Exception("EventArgUtil: Invalid gameEvent Type: " + gameEvent.Type + " in GetTargetTexture"); }
            return GuiTextureTypeUtil.GetTypeFromString((string)gameEvent.GetArg("TextureName"));
        }

        // MoveCommand
        public static Vector GetDirection(GameEvent gameEvent)
        {
            if (gameEvent.Type != EventType.MoveCommand) { throw new Exception("EventArgUtil: Invalid gameEvent Type: " + gameEvent.Type + " in GetDirection"); }
            try
            {
                return (Vector) gameEvent.GetArg("Direction");
            }
            catch (FormatException) { throw new Exception("EventArgUtil: Invalid X,Y value in GetDirection: " + gameEvent.Source.ToString()); }
        }

        public static double GetSpeed(GameEvent gameEvent)
        {
            if (gameEvent.Type != EventType.MoveCommand) { throw new Exception("EventArgUtil: Invalid gameEvent Type: " + gameEvent.Type + " in GetSpeed"); }
            try { return (double) gameEvent.GetArg("Speed"); }
            catch (FormatException) { throw new Exception("EventArgUtil: Invalid Speed value in GetSpeed: " + gameEvent.Source.ToString()); }
        }

        // SetPosition (Absolute/Relative)
        public static Vector GetPositionVector(GameEvent gameEvent)
        {
            if (gameEvent.Type != EventType.SetPositionCommand) { throw new Exception("EventArgUtil: Invalid gameEvent Type: " + gameEvent.Type + " in GetVector"); }
            try { return (Vector)gameEvent.GetArg("PositionVector"); }
            catch (FormatException) { throw new Exception("EventArgUtil: Invalid value in GetPosition: " + gameEvent.Source.ToString()); }
        }

        public static bool GetIsRelative(GameEvent gameEvent) // True by default; set "isRelative = false" if you want to move an object to a specific OpenGL position
        {
            if (gameEvent.Type != EventType.SetPositionCommand) { throw new Exception("EventArgUtil: Invalid gameEvent Type: " + gameEvent.Type + " in GetIsRelative"); }
            try { return (bool)gameEvent.GetArg("IsRelative"); }
            catch (FormatException) { throw new Exception("EventArgUtil: Invalid IsRelative value in GetIsRelative: " + gameEvent.Source.ToString()); }
        }

    }
}
