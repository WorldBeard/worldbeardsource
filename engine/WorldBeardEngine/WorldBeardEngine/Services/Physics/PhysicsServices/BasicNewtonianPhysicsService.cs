using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine.Services.Physics.PhysicsServices
{
    public class BasicNewtonianPhysicsService : PhysicsService
    {
        public BasicNewtonianPhysicsService() : base() { } // For reflection

        protected override void ApplyPhysics(PhysicsComponent component)
        {
            component.YVelocity += _currentElapsedTime * ConfigSettings.GRAVITY_PPS;
            component.XVelocity = CheckMaxVelocity(component.XVelocity, ConfigSettings.MAX_X_VELOCITY);
            component.YVelocity = CheckMaxVelocity(component.YVelocity, ConfigSettings.MAX_Y_VELOCITY);
        }

    }
}
