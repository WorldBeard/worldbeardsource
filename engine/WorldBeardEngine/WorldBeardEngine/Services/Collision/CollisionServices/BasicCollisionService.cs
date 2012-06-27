using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;

namespace WorldBeardEngine.Services.Collision.CollisionServices
{
    public class BasicCollisionService : CollisionService
    {
        private Dictionary<double, Collision> _previousCollisions = new Dictionary<double, Collision>();

        // Private inner class used to track past collisions.
        private class Collision
        {
            internal double ID1;
            internal double ID2;
            internal double Hash { get { return ID1 + (ID2 / 1000000); } } // Unique hash presumes no obj ID > 1,000,000.
            internal Collision(int id1, int id2)
            {
                ID1 = id1;
                ID2 = id2;
            }
            internal static double GetHash(int id1, int id2) { return ((double)id1) + (((double)id2) / 1000000); }
        }

        public BasicCollisionService() : base() { } // For reflection.

        public override void Update(double elapsedTime)
        {
            Collision newCollision;
            // Loop through all CollisionComponents, checking them in pairs to see if they're colliding.
            for (int i = 0; i < _registrees.Count; i++)
            {
                for (int j = i + 1; j < _registrees.Count; j++)
                {
                    CollisionComponent comp1 = (CollisionComponent)_registrees[i];
                    CollisionComponent comp2 = (CollisionComponent)_registrees[j];
                    double tempHash = Collision.GetHash(comp1.ID, comp2.ID);

                    if (CollisionDetector.AreColliding(comp1, comp2))
                    {
                        // If these objects were already colliding last frame, don't log or send events.
                        if (_previousCollisions.ContainsKey(tempHash)) { continue; }

                        // If they weren't colliding last frame, add to list of currently colliding objects, log, and send events.
                        newCollision = new Collision(comp1.ID, comp2.ID);
                        _previousCollisions.Add(newCollision.Hash, newCollision);
                        SingletonFactory.LOG.Log("Collision between objs " + StringUtil.PadLeft(comp1.ID, 5, '0') + " and " + StringUtil.PadLeft(comp2.ID, 5, '0'), ComponentType.Collision);
                        SingletonFactory.GetEventHub().SendEvent(SingletonFactory.GetEventFactory().GetGameEvent("Colliding", comp2), comp1);
                        SingletonFactory.GetEventHub().SendEvent(SingletonFactory.GetEventFactory().GetGameEvent("Colliding", comp1), comp2);
                    }
                    else
                    {
                        // If they're not colliding, remove them from the list of currently colliding objects, if applicable.
                        if(_previousCollisions.ContainsKey(tempHash)) { _previousCollisions.Remove(tempHash); }
                    }
                }
            }
        }

    }
}
