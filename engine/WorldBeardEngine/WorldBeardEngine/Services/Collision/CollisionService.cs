using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;

namespace WorldBeardEngine.Services.Collision
{
    public abstract class CollisionService : Service
    {

        public CollisionService()
        {
            _validRegisterType = ComponentType.Collision;
        }

        public abstract override void Update(double elapsedTime);

    }
}
