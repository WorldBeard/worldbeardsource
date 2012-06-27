using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Events;
using WorldBeardEngine.Services.Movement;
using WorldBeardEngine.Services.Rendering;
using WorldBeardEngine.Services.Rendering.RenderingTypes;
using EarthCoreEngine;

namespace WorldBeardEngine.EventHandlers
{
    internal class HandlerDelegateFactory
    {
        internal GameEventHandler.HandleEvent GetDelegate(string handlerName)
        {
            switch (handlerName.ToUpper())
            {
                case "CHANGETEXTURE":
                    return GetChangeTextureDelegate();
                case "MOVEME":
                    return GetMoveMeDelegate();
                case "RESIZESPRITE":
                    return GetResizeSpriteDelegate();
                case "SETPOSITION":
                    return GetSetPositionDelegate();
                default:
                    throw new Exception("handlerName not recognized by EventHandlerFactory: " + handlerName);
            }
        }

        // CHANGETEXTURE - Change a RenderingComponent's texture
        private GameEventHandler.HandleEvent GetChangeTextureDelegate() { return new GameEventHandler.HandleEvent(HandlerDelegateFactory.ChangeTexture); }
        private static void ChangeTexture(Component parent, GameEvent gameEvent, Dictionary<string, Object> handlerArgs)
        {
            if (parent is GuiRenderingComponent)
            {
                GuiRenderingComponent guiRenParent = (GuiRenderingComponent)parent;
                guiRenParent.SetTexture(EventArgReadUtil.GetGuiTextureType(gameEvent));
            }
            else
            {
                MultiTextureRenderingComponent multiTexParent = (MultiTextureRenderingComponent)parent;
                multiTexParent.SetTexture(EventArgReadUtil.GetTexture(gameEvent));
            }
        }

        // MOVEME - Set a MovementComponent's speed and direction
        private GameEventHandler.HandleEvent GetMoveMeDelegate() { return new GameEventHandler.HandleEvent(HandlerDelegateFactory.MoveMe); }
        private static void MoveMe(Component parent, GameEvent gameEvent, Dictionary<string, Object> handlerArgs)
        {
            // Speed and direction should come from the event
            MovementComponent movementParent = (MovementComponent)parent;
            movementParent.Direction = EventArgReadUtil.GetDirection(gameEvent);
            movementParent.Speed = EventArgReadUtil.GetSpeed(gameEvent);
        }

        // RESIZE SPRITE - Resize the RenderingComponent's Sprite
        private GameEventHandler.HandleEvent GetResizeSpriteDelegate() { return new GameEventHandler.HandleEvent(HandlerDelegateFactory.ResizeSprite); }
        private static void ResizeSprite(Component parent, GameEvent gameEvent, Dictionary<string, Object> handlerArgs)
        {
            SingletonFactory.LOG.Log("obj " + StringUtil.PadLeft(parent.ID, 5, '0') + " resized to " + handlerArgs["Height"] + "x" + handlerArgs["Width"], ComponentType.Rendering);
            RenderingComponent renderingParent = (RenderingComponent)parent;
            renderingParent.Model.Height = (double) handlerArgs["Height"];
            renderingParent.Model.Width = (double) handlerArgs["Width"];
        }

        // SET POSITION - Change the position of the RenderingComponent's Sprite, either in absolute OpenGL coordinates, or relative
        //                to the Sprite's current position
        private GameEventHandler.HandleEvent GetSetPositionDelegate() { return new GameEventHandler.HandleEvent(HandlerDelegateFactory.SetPosition); }
        private static void SetPosition(Component parent, GameEvent gameEvent, Dictionary<string, Object> handlerArgs)
        {
            RenderingComponent renderingParent = (RenderingComponent)parent;
            if (!EventArgReadUtil.GetIsRelative(gameEvent)) { renderingParent.Model.SetPosition(EventArgReadUtil.GetPositionVector(gameEvent)); }
            else { renderingParent.Model.Center += EventArgReadUtil.GetPositionVector(gameEvent); }
        }

    }
}
