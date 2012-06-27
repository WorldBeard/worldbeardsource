using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Services.Physics;

namespace WorldBeardEngine.Load.ComponentFactories
{
    class PhysicsFactory : IComponentFactory
    {
        public Component GetComponent(ComponentDO componentDO)
        {
            switch (componentDO.Subclass.ToUpper())
            {
                case "BASIC":
                    return new PhysicsComponent(componentDO.Subclass);
                default:
                    throw new Exception("Physics subclass not recognized by PhysicsFactory: " + componentDO.Subclass);
            }
        }
    }
}
