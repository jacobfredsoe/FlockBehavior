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
        private Random rand;
        private Vector2 influenceVector;

        public Boid(Texture2D texture, Vector2 location)
        {
            rand = new Random();
            direction = new Vector2(0, -1);
            speed = 2.5f;
            angle = 0;
            this.location = location;
            maxTurnSpeed = 0.05f;

            influenceVector = new Vector2(0, 0);
            this.texture = texture;

            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void Update(GameTime gameTime, MouseState mouse)
        {
            //Find and turn the boid in the correct direction
            if (Constants.findAngle(direction, influenceVector) < Math.PI) direction = Constants.rotateRadians(direction, maxTurnSpeed);
            else direction = Constants.rotateRadians(direction, -maxTurnSpeed);

            //Set the angle of the texture to follow
            angle = Constants.findAngle(new Vector2(0, -1), direction);

            //Move the boid
            Vector2 velocity = direction;
            velocity.Normalize();
            location += velocity * speed;

            FlockBehavior.sendMessage("Velocity: " + velocity.ToString());
            FlockBehavior.sendMessage("MouseVector: " + influenceVector.ToString());

            //Reset the influence
            influenceVector.X = 0;
            influenceVector.Y = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
        }

        public void influenceBoid(Point pointOfInterest)
        {
            influenceVector += new Vector2(pointOfInterest.X - location.X, pointOfInterest.Y - location.Y);
        }
    }
}
