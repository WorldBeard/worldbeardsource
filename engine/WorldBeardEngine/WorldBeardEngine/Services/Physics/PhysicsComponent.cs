using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Services.Movement;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Physics
{
    public class PhysicsComponent : Component
    {
        private MovementComponent _parentMoveComponent { get { return (MovementComponent)_parent.GetComponent(ComponentType.Movement); } }

        public double XVelocity {
            get
            {
                return _parentMoveComponent.XVelocity;
            }
            set
            {
                Vector movementVector = new Vector(value, _parentMoveComponent.YVelocity);
                _parentMoveComponent.Direction = Vector.Normalize(movementVector);
                _parentMoveComponent.Speed = movementVector.Length;
            }
        }

        public double YVelocity
        {
            get
            {
                return _parentMoveComponent.YVelocity;
            }
            set
            {
                Vector movementVector = new Vector(_parentMoveComponent.XVelocity, value);
                _parentMoveComponent.Direction = Vector.Normalize(movementVector);
                _parentMoveComponent.Speed = movementVector.Length;
            }
        }

        public double Speed { get { return _parentMoveComponent.Speed; } set { _parentMoveComponent.Speed = value; } }
        public Vector Direction { get { return _parentMoveComponent.Direction; } set { _parentMoveComponent.Direction = value; } }

        public PhysicsComponent(string subclass) : base(subclass, ComponentType.Physics) { }

        public override void Dispose()
        {
            // INTENTIONIALLY BLANK
        }
    }
}
