using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FlockBehavior
{
    static class Constants
    {
        public const int WINDOW_HEIGHT = 800;
        public const int WINDOW_WIDTH = 800;
        public const int BORDER_DISTANCE = 100;

        public enum EffectState { attract, scare };

        public const float BOID_SPEED_RANGE = 2.0f;
        public const float BOID_SPEED_MIN = 2.0f;
        public const float BOID_TURNSPEED = 0.15f;

        public const float GLOCAL_INFLUENCE_DISTANCE = 100;
        public const float SPARROW_INFLUENCE_DISTANCE = 50;


        /// <summary>
        /// Finds clockwise angle between two vectors
        /// </summary>
        /// <param name="v1">vector 1</param>
        /// <param name="v2">vector 2</param>
        /// <returns>radians between the vectors</returns>
        public static float findAngle(Vector2 v1, Vector2 v2)
        {
            v1 = new Vector2(v1.X, -v1.Y);
            v2 = new Vector2(v2.X, -v2.Y);

            float dotProduct = Vector2.Dot(v1, v2);
            float determinant = v1.X * v2.Y - v1.Y * v2.X;
            float magnitude = v1.Length() * v2.Length();

            if (determinant > 0) return (float)(2 * Math.PI - Math.Acos(dotProduct / magnitude));
            else return (float)Math.Acos(dotProduct / magnitude);
        }

        /// <summary>
        /// Rotate a vector an amount of radians
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="radians">radian to turn</param>
        /// <returns>new vector that has been turned</returns>
        public static Vector2 rotateRadians(Vector2 v, double radians)
        {
            float ca = (float)Math.Cos(radians);
            float sa = (float)Math.Sin(radians);
            return new Vector2(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y);
        }

        /// <summary>
        /// Adjust a point so that it is moved according to how close it is to the object
        /// </summary>
        /// <param name="unadjustedPoint">The point that is to be moved</param>
        /// <param name="distance">how close it is to the object</param>
        /// <param name="influenceDistance">how close the other object has to be, before considdering it</param>
        /// <returns></returns>
        public static Point distanceAdjust(Point unadjustedPoint, float distance, float influenceDistance)
        {
            float proximityAdjusting = 1 / (distance / influenceDistance);

            Point adjustedPoint = new Point((int)(unadjustedPoint.X * proximityAdjusting), (int)(unadjustedPoint.Y * proximityAdjusting));

            return adjustedPoint;
        }

        /// <summary>
        /// Overload method
        /// </summary>
        /// <param name="unadjustedPoint"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static Point distanceAdjust(Point unadjustedPoint, float distance)
        {
            return distanceAdjust(unadjustedPoint, distance, GLOCAL_INFLUENCE_DISTANCE);
        }
    }
}
