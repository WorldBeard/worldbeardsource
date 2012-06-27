using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Services.Collision.CollisionTypes;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Collision
{
    class CollisionDetector
    {

        public static Boolean AreColliding(CollisionComponent comp1, CollisionComponent comp2)
        {

            if (comp1 is CollisionRectangle && comp2 is CollisionRectangle)
            {
                return RectRect((CollisionRectangle)comp1, (CollisionRectangle)comp2);
            }
            else if (comp1 is CollisionRectangle && comp2 is CollisionCircle)
            {
                return RectCirc((CollisionRectangle)comp1, (CollisionCircle)comp2);
            }
            else if (comp1 is CollisionCircle && comp2 is CollisionRectangle)
            {
                // n.b. - arguments are reversed
                return RectCirc((CollisionRectangle)comp2, (CollisionCircle)comp1);
            }
            else if (comp1 is CollisionCircle && comp2 is CollisionCircle)
            {
                return CircCirc((CollisionCircle)comp1, (CollisionCircle)comp2);
            }

            throw new Exception("CollisionDetector.AreColliding cannot handle arguments: " + comp1 + " and " + comp2);
        }

        private static Boolean RectRect(CollisionRectangle rect1, CollisionRectangle rect2)
        {
            double rect1Radius = (rect1.TopLeft - rect1.Center).Length;
            double rect2Radius = (rect2.TopLeft - rect2.Center).Length;
            double distanceBetweenCenters = (rect1.Center - rect2.Center).Length;
            if (distanceBetweenCenters >= rect1Radius + rect2Radius) { return false; }

            if (IsSeparatingAxis(rect1.TopLeft, rect1.TopRight, rect1.BottomRight, rect2)) { return false; }
            if (IsSeparatingAxis(rect1.TopRight, rect1.BottomRight, rect1.BottomLeft, rect2)) { return false; }
            if (IsSeparatingAxis(rect1.BottomRight, rect1.BottomLeft, rect1.TopLeft, rect2)) { return false; }
            if (IsSeparatingAxis(rect1.BottomLeft, rect1.TopLeft, rect1.TopRight, rect2)) { return false; }

            if (IsSeparatingAxis(rect2.TopLeft, rect2.TopRight, rect2.BottomRight, rect1)) { return false; }
            if (IsSeparatingAxis(rect2.TopRight, rect2.BottomRight, rect2.BottomLeft, rect1)) { return false; }
            if (IsSeparatingAxis(rect2.BottomRight, rect2.BottomLeft, rect2.TopLeft, rect1)) { return false; }
            if (IsSeparatingAxis(rect2.BottomLeft, rect2.TopLeft, rect2.TopRight, rect1)) { return false; }

            return true;
        }

        private static bool IsSeparatingAxis(Vector sepAxisPoint1, Vector sepAxisPoint2, Vector thirdPoint, CollisionRectangle otherRectangle)
        {
            Vector[] otherQuadPoints = new Vector[4];
            otherQuadPoints[0] = otherRectangle.TopLeft;
            otherQuadPoints[1] = otherRectangle.TopRight;
            otherQuadPoints[2] = otherRectangle.BottomLeft;
            otherQuadPoints[3] = otherRectangle.BottomRight;

            Vector separatingAxis = sepAxisPoint2 - sepAxisPoint1;
            Vector normalAxis = new Vector(separatingAxis.Y, -separatingAxis.X);
            bool referenceSign = normalAxis * (thirdPoint - sepAxisPoint1) >= 0;

            foreach (Vector cornerOfSecondRect in otherQuadPoints)
            {
                bool sign = normalAxis * (cornerOfSecondRect - sepAxisPoint1) >= 0;
                // A point of the other quad is one the same side as thirdPoint
                if (sign == referenceSign) { return false; }
            }
            // All points of the other quad are on the other side of the edge. Therefore the edge is a separating axis
            return true;
        }

        private static Boolean RectCirc(CollisionRectangle rectangle, CollisionCircle circle)
        {
            // Transform rectangle corner coordinates and circle center to be axis-aligned
            Matrix rotationMatrix = new Matrix();
            rotationMatrix.Set2DRotate(-1 * rectangle.Rotation);
            Vector rotatedTopLeft = rectangle.TopLeft * rotationMatrix;
            Vector rotatedTopRight = rectangle.TopRight * rotationMatrix;
            Vector rotatedBottomLeft = rectangle.BottomLeft * rotationMatrix;
            Vector rotatedBottomRight = rectangle.BottomRight * rotationMatrix;
            Vector rotatedCircleCenter = circle.Center * rotationMatrix;
            double circRadius = circle.Radius;
            Vector rotatedRectangleCenter = rotatedTopLeft + (rotatedBottomRight - rotatedTopLeft).Multiply(0.5);
            double rectPercent = rectangle.CollisionPercent;

            // Use of doubles and matrix operations can cause very very small discrepencies that should be ignored
            if (Math.Abs(rotatedTopLeft.Y - rotatedTopRight.Y) > 0.00000001)
            {
                throw new Exception("Coordinate transform failed in CollisionDetector.RectCirc.  TopLeft is " + rotatedTopLeft + " and TopRight is "+ rotatedTopRight);
            }

            bool isCircCenterOutsideRect = true;

            double rectHalfWidth = 0.5 * Math.Abs(rotatedTopRight.X - rotatedTopLeft.X) * rectPercent;
            double halfWidthPlusRadius = rectHalfWidth + circRadius;
            double xSeparation = Math.Abs(rotatedCircleCenter.X - rotatedRectangleCenter.X);
            if (xSeparation >= halfWidthPlusRadius) { return false; }
            if (xSeparation < rectHalfWidth) { isCircCenterOutsideRect = false; }

            double rectHalfHeight = 0.5 * Math.Abs(rotatedTopRight.Y - rotatedBottomRight.Y) * rectPercent;
            double halfHeightPlusRadius = rectHalfWidth + circRadius;
            double ySeparation = Math.Abs(rotatedCircleCenter.Y - rotatedRectangleCenter.Y);
            if (ySeparation >= halfHeightPlusRadius) { return false; }
            if (ySeparation < rectHalfHeight) { isCircCenterOutsideRect = false; }

            if (!isCircCenterOutsideRect) { return true; }

            // Check for literal corner cases
            Vector closestCorner;
            if (rotatedCircleCenter.X > rotatedRectangleCenter.X && rotatedCircleCenter.Y > rotatedRectangleCenter.Y) { closestCorner = rotatedTopRight; }
            else if (rotatedCircleCenter.X > rotatedRectangleCenter.X && rotatedCircleCenter.Y <= rotatedRectangleCenter.Y) { closestCorner = rotatedBottomRight; }
            else if (rotatedCircleCenter.Y > rotatedRectangleCenter.Y) { closestCorner = rotatedTopLeft; }
            else { closestCorner = rotatedBottomLeft; }

            if ((closestCorner - rotatedCircleCenter).LengthSquared > circRadius * circRadius) { return false; }

            return true;
        }

        private static Boolean CircCirc(CollisionCircle comp1, CollisionCircle comp2)
        {
            double rad1 = comp1.Radius;
            double rad2 = comp2.Radius;
            double rad1Squared = rad1 * rad1;
            double rad2Squared = rad2 * rad2;

            double distanceSquared = (comp1.Center - comp2.Center).LengthSquared;

            if (distanceSquared >= rad1Squared + rad2Squared)
            {
                return false;
            }
            return true;
        }

    }
}
