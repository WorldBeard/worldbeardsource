using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;
using WorldBeardEngine.Services.GUI;

namespace WorldBeardEngine.Services.Rendering.RenderingTypes
{
    public class GuiRenderingComponent : MultiTextureRenderingComponent
    {

        private Dictionary<GuiTextureType, Texture> _textureDictionary = new Dictionary<GuiTextureType, Texture>();

        public GuiRenderingComponent(string subclass, Sprite model) : this(subclass, model, "") { }

        public GuiRenderingComponent(string subclass, Sprite model, string mouseOverTextureName) : this(subclass, model, mouseOverTextureName, "") { }

        public GuiRenderingComponent(string subclass, Sprite model, string mouseOverTextureName, string mouseDownTextureName)
            : base(subclass, model)
        {
            _textureDictionary.Add(GuiTextureType.NULL, model.Texture);
            if(mouseOverTextureName != "") { AddTexture(GuiTextureType.MOUSE_OVER, mouseOverTextureName); }
            if (mouseDownTextureName != "") { AddTexture(GuiTextureType.MOUSE_DOWN, mouseDownTextureName); }

            SingletonFactory.GetEventHandlerDecorator().AddHandler(this, "ChangeTexture", Events.EventType.ChangeTextureCommand, null);
        }

        public void AddTexture(GuiTextureType guiType, string textureName)
        {
            _textureDictionary.Add(guiType, SingletonFactory.GetTextureManager().Get(textureName));
        }

        public void SetTexture(GuiTextureType guiType)
        {
            _model.Texture = _textureDictionary[guiType];
        }

    }
}
