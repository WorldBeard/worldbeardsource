using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Services.Movement;
using WorldBeardEngine.Services.Movement.MovementTypes;
using EarthCoreEngine;

namespace WorldBeardEngine.Load.ComponentFactories
{
    class MovementFactory : IComponentFactory
    {
        public Component GetComponent(ComponentDO componentDO)
        {
            switch (componentDO.Subclass.ToUpper())
            {
                case "BASIC":
                    return new BasicMovement("MovementComp", ComponentDOUtil.GetDirectionVector(componentDO), ComponentDOUtil.GetSpeed(componentDO));
                case "CIRCLE":
                    return new CircleMovement("MovementComp", ComponentDOUtil.GetDirectionVector(componentDO), ComponentDOUtil.GetSpeed(componentDO), int.Parse(componentDO.Properties["Radius"]), int.Parse(componentDO.Properties["TimeToRotate"]));
                case "FORWARDFACING":
                    return new ForwardFacingMovement("MovementComp", ComponentDOUtil.GetDirectionVector(componentDO), ComponentDOUtil.GetSpeed(componentDO));
                default:
                    throw new Exception("Movement subclass not recognized by MovementFactory: " + componentDO.Subclass);
            }
        }
    }
}
