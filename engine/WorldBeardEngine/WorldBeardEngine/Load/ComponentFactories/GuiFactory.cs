using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Services.GUI;

namespace WorldBeardEngine.Load.ComponentFactories
{
    class GuiFactory : IComponentFactory
    {

        public Component GetComponent(ComponentDO componentDO)
        {
            switch (componentDO.Subclass.ToUpper())
            {
                case "BASIC":
                    return new GuiComponent("Basic", ComponentDOUtil.GetIsClickable(componentDO));
                default:
                    throw new Exception("GUI subclass not recognized by GuiFactory: " + componentDO.Subclass);
            }
        }

    }
}
