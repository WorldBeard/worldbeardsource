using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Services.Collision;
using WorldBeardEngine.Services.Collision.CollisionTypes;

namespace WorldBeardEngine.Load.ComponentFactories
{
    class CollisionFactory : IComponentFactory
    {
        public Component GetComponent(ComponentDO componentDO)
        {
            switch (componentDO.Subclass.ToUpper())
            {
                case "BASIC":
                    return new CollisionRectangle(componentDO.Subclass, ComponentDOUtil.GetCollisionPercent(componentDO));
                case "CIRCLE":
                    return new CollisionCircle(componentDO.Subclass, ComponentDOUtil.GetCollisionRadius(componentDO));
                case "RECTANGLE":
                    return new CollisionRectangle(componentDO.Subclass, ComponentDOUtil.GetCollisionPercent(componentDO));
                default:
                    throw new Exception("Collision subclass not recognized by CollisionFactory: " + componentDO.Subclass);
            }
        }
    }
}
