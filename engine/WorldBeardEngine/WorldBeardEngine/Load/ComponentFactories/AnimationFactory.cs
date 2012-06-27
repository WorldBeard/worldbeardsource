using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Services.Animation;

namespace WorldBeardEngine.Load.ComponentFactories
{
    class AnimationFactory : IComponentFactory
    {
        public Component GetComponent(ComponentDO componentDO)
        {
            switch (componentDO.Subclass.ToUpper())
            {
                case "BASIC":
                    return new AnimationComponent(componentDO.Subclass, ComponentDOUtil.GetAnimationFramesX(componentDO), ComponentDOUtil.GetAnimationFramesY(componentDO));
                default:
                    throw new Exception("Animation subclass not recognized by AnimationFactory: " + componentDO.Subclass);
            }
        }
    }
}
