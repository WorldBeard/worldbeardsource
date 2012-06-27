using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;

namespace WorldBeardEngine.Services.Physics
{
    public abstract class PhysicsService : Service
    {
        protected double _currentElapsedTime = 0;

        public PhysicsService()
        {
            _validRegisterType = ComponentType.Physics;
        }

        public override void Update(double elapsedTime)
        {
            if (_registrees.Count == 0) { return; }
            foreach (PhysicsComponent component in _registrees)
            {
                _currentElapsedTime = elapsedTime;
                ApplyPhysics(component);
            }
        }

        protected abstract void ApplyPhysics(PhysicsComponent component);

        protected double CheckMaxVelocity(double speed, double max)
        {
            return Math.Abs(speed) > max ? max * Math.Sign(speed) : speed;
        }

    }
}
