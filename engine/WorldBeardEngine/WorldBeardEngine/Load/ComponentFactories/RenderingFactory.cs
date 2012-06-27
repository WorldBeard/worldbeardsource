using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Services.GUI;
using WorldBeardEngine.Services.Rendering;
using WorldBeardEngine.Services.Rendering.RenderingTypes;

namespace WorldBeardEngine.Load.ComponentFactories
{
    class RenderingFactory : IComponentFactory
    {
        public Component GetComponent(ComponentDO componentDO)
        {
            switch (componentDO.Subclass.ToUpper())
            {
                case "BASIC":
                    return new RenderingComponent(componentDO.Subclass, ComponentDOUtil.GetSprite(componentDO));
                case "MULTITEXTURE":
                    return new MultiTextureRenderingComponent(componentDO.Subclass, ComponentDOUtil.GetSprite(componentDO));
                case "GUI":
                    GuiRenderingComponent comp = new GuiRenderingComponent(componentDO.Subclass, ComponentDOUtil.GetSprite(componentDO));
                    comp.AddTexture(GuiTextureType.MOUSE_OVER, componentDO.Properties["MouseOverTextureName"]);
                    comp.AddTexture(GuiTextureType.MOUSE_DOWN, componentDO.Properties["MouseDownTextureName"]);
                    return comp;
                default:
                    throw new Exception("Rendering subclass not recognized by RenderingFactory: " + componentDO.Subclass);
            }
        }
    }
}
