using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlockBehavior
{
    class Boid
    {
        private Vector2 direction;
        private float speed;
        private Texture2D texture;
        private Vector2 location;
        private float angle;
        private Rectangle sourceRectangle;
        private Vector2 origin;
        private float maxTurnSpeed;

        public Boid(Texture2D texture)
        {
            direction = new Vector2(0, -2);
            speed = 2.5f;
            angle = 0;
            location = new Vector2(Constants.WINDOW_WIDTH / 2, Constants.WINDOW_HEIGHT / 2);
            maxTurnSpeed = 0.05f;
            

            this.texture = texture;

            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void Update(GameTime gameTime, MouseState mouse)
        {
            Vector2 velocity = direction;
            velocity.Normalize();
            location += velocity * speed;

            
            //direction = RotateRadians(direction, maxTurnSpeed);
            if (mouse.LeftButton == ButtonState.Pressed) direction = RotateRadians(direction, maxTurnSpeed);
            

            Vector2 mouseVector = new Vector2(mouse.X - location.X, mouse.Y - location.Y);


            if (findAngle(velocity, mouseVector) < Math.PI) direction = RotateRadians(direction, maxTurnSpeed);
            else direction = RotateRadians(direction, -maxTurnSpeed);

            angle = findAngle(new Vector2(0, -1), direction);

            FlockBehavior.sendMessage("Velocity: " + velocity.ToString());
            FlockBehavior.sendMessage("MouseVector: " + mouseVector.ToString());
            FlockBehavior.sendMessage("Angle: " + MathHelper.ToDegrees(findAngle(velocity, mouseVector)));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
        }

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

        public void influenceBoid(Point pointOfInterest)
        {
            //Move towards that point
        }

        public Vector2 RotateRadians(Vector2 v, double radians)
        {
            float ca = (float)Math.Cos(radians);
            float sa = (float)Math.Sin(radians);
            return new Vector2(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y);
        }
    }
}
