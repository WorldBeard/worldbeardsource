using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;
using WorldBeardEngine.Components;

namespace WorldBeardEngine.Services.Animation
{
    public class AnimationService : Service
    {

        public AnimationService()
        {
            _validRegisterType = ComponentType.Animation;
        }

        public override void Update(Double elapsedTime)
        {
            if (_registrees.Count == 0) { return; }

            foreach (AnimationComponent component in _registrees)
            {
                Animate(component, elapsedTime);
            }
        }

        private void Animate(AnimationComponent component, double elapsedTime)
        {
            if (component.Parent.HasComponent(ComponentType.Rendering))
            {
                component.Model.Update(elapsedTime);
            }
            else
            {
                throw new Exception("Attempting to animate AnimationComponent with no RenderingComponent: " + component.FullName);
            }
        }

    }
}
