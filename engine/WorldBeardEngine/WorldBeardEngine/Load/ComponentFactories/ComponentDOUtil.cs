using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;

namespace WorldBeardEngine.Load.ComponentFactories
{
    class ComponentDOUtil
    {

        // ANIMATION
        internal static int GetAnimationFramesX(ComponentDO componentDO)
        {
            return int.Parse(componentDO.Properties["FramesX"]);
        }

        internal static int GetAnimationFramesY(ComponentDO componentDO)
        {
            return int.Parse(componentDO.Properties["FramesY"]);
        }

        // COLLISION
        internal static string GetCollisionShape(ComponentDO componentDO)
        {
            return componentDO.Properties["CollisionShape"];
        }

        internal static double GetCollisionRadius(ComponentDO componentDO)
        {
            try { return double.Parse(componentDO.Properties["Radius"]); }
            catch (KeyNotFoundException) { return -1; }
            catch (FormatException) { throw new Exception("Could not parse Collision/Radius: " + componentDO.Properties["Radius"]); }
        }

        internal static double GetCollisionPercent(ComponentDO componentDO)
        {
            try { return double.Parse(componentDO.Properties["CollisionPercent"]); }
            catch (KeyNotFoundException) { return 1; }
            catch (FormatException) { throw new Exception("Could not parse Collision/CollisionPercent: " + componentDO.Properties["CollisionPercent"]); }
        }

        // GUI
        internal static bool GetIsClickable(ComponentDO componentDO)
        {
            try { return bool.Parse(componentDO.Properties["IsClickable"]); }
            catch (KeyNotFoundException) { return false; }
            catch (FormatException) { throw new Exception("Could not parse IsClickable: " + componentDO.Properties["IsClickable"]); }
        }

        // MOVEMENT
        internal static Vector GetDirectionVector(ComponentDO componentDO)
        {
            double x;
            double y;

            try { x = double.Parse(componentDO.Properties["X"]); }
            catch (KeyNotFoundException) { x = 0; }
            catch (FormatException) { throw new Exception("Could not parse Movement/XDirection: " + componentDO.Properties["X"]); }

            try { y = double.Parse(componentDO.Properties["Y"]); }
            catch (KeyNotFoundException) { y = 0; }
            catch (FormatException) { throw new Exception("Could not parse Movement/YDirection: " + componentDO.Properties["Y"]); }

            return new Vector(x, y);
        }

        internal static double GetSpeed(ComponentDO componentDO)
        {
            try { return double.Parse(componentDO.Properties["Speed"]); }
            catch (KeyNotFoundException) { return ConfigSettings.DEFAULT_NON_PLAYER_SPEED; }
            catch (FormatException) { throw new Exception("Could not parse Movement/Speed: " + componentDO.Properties["Speed"]); }
        }

        // RENDERING
        internal static Sprite GetSprite(ComponentDO componentDO)
        {
            string textureName;
            int startingX;
            int startingY;
            int height;
            int width;

            try { textureName = componentDO.Properties["TextureName"]; }
            catch (KeyNotFoundException) { throw new Exception("Must provide Rendering/TextureName"); }

            try { startingX = int.Parse(componentDO.Properties["X"]); }
            catch (KeyNotFoundException) { startingX = 0; }
            catch (FormatException) { throw new Exception("Could not parse Rendering/X: " + componentDO.Properties["X"]); }

            try { startingY = int.Parse(componentDO.Properties["Y"]); }
            catch (KeyNotFoundException) { startingY = 0; }
            catch (FormatException) { throw new Exception("Could not parse Rendering/Y: " + componentDO.Properties["Y"]); }

            try { height = int.Parse(componentDO.Properties["Height"]); }
            catch (KeyNotFoundException) { height = -1; }
            catch (FormatException) { throw new Exception("Could not parse Rendering/Height: " + componentDO.Properties["Height"]); }

            try { width = int.Parse(componentDO.Properties["Width"]); }
            catch (KeyNotFoundException) { width = -1; }
            catch (FormatException) { throw new Exception("Could not parse Rendering/Width: " + componentDO.Properties["Width"]); }

            return SingletonFactory.GetTextureManager().GetAnimatedSprite(textureName, startingX, startingY, width, height);
        }

    }
}
