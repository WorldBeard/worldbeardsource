using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Services.Rendering;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Movement
{
    public class MovementService : Service
    {

        public MovementService()
        {
            _validRegisterType = ComponentType.Movement;
        }

        public override void Update(Double elapsedTime)
        {
            if (_registrees.Count == 0) { return; }

            foreach (MovementComponent component in _registrees)
            {
                component.Move(elapsedTime);
            }
        }

    }
}
