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

        public enum EffectState { attract, scare };

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
    }
}
