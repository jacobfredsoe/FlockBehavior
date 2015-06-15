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
        private float turnspeed;
        private Vector2 influenceVector;
        private Vector2 borders;

        public Boid(Texture2D texture, Vector2 location, float speed, float turnspeed, Vector2 borders)
        {
            direction = new Vector2(0, -1);
            angle = 0;
            influenceVector = new Vector2(0, 0);

            this.speed = speed;
            this.location = location;
            this.turnspeed = turnspeed;
            this.texture = texture;
            this.borders = borders;

            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void Update(GameTime gameTime, MouseState mouse)
        {
            Cage();
            if(influenceVector.Length() == 0)
            {
                influenceVector = direction * speed;
            }
            //Find and turn the boid in the correct direction
            if (Constants.findAngle(direction, influenceVector) < Math.PI) direction = Constants.rotateRadians(direction, turnspeed);
            else direction = Constants.rotateRadians(direction, -turnspeed);

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

        private void Cage()
        {
            if (location.X > borders.X - Constants.BORDER_DISTANCE)
            {
                influenceBoid(new Point(0, (int)location.Y));
            }
            else if(location.X < Constants.BORDER_DISTANCE)
            {
                influenceBoid(new Point((int)borders.X, (int)location.Y));
            }
            else if(location.Y > borders.Y - Constants.BORDER_DISTANCE)
            {
                influenceBoid(new Point((int)location.X, 0));
            }
            else if(location.Y < Constants.BORDER_DISTANCE)
            {
                influenceBoid(new Point((int)borders.Y, (int)location.X));
            }


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
