using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Services.Input;
using WorldBeardEngine.Services.Input.InputTypes;

namespace WorldBeardEngine.Load.ComponentFactories
{
    class InputFactory : IComponentFactory
    {
        public Component GetComponent(ComponentDO componentDO)
        {
            switch (componentDO.Subclass.ToUpper())
            {
                case "BASIC":
                    return new BasicPlayerInput(componentDO.Subclass);
                case "CLICKTOMOVE":
                    return new ClickToMoveInput(componentDO.Subclass);
                default:
                    throw new Exception("Input subclass not recognized by InputFactory: " + componentDO.Subclass);
            }
        }
    }
}
