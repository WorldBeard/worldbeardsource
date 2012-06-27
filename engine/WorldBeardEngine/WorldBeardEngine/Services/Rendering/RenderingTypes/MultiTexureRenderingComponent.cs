using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Rendering.RenderingTypes
{
    public class MultiTextureRenderingComponent : RenderingComponent
    {

        public MultiTextureRenderingComponent(String subclass, Sprite model) : base(subclass, model) { }

        public void SetTexture(string textureName)
        {
            _model.Texture = SingletonFactory.GetTextureManager().Get(textureName);
        }

    }
}
