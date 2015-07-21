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

        public Vector2 Location
        {
            get {  return location;}
        }

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

        public void Update(GameTime gameTime)
        {
            Cage();

            if(influenceVector.Length() != 0)
            {
                //Find and turn the boid in the correct direction
                if (Constants.findAngle(direction, influenceVector) < Math.PI) direction = Constants.rotateRadians(direction, turnspeed);
                else direction = Constants.rotateRadians(direction, -turnspeed);
            }

            //Set the angle of the texture to follow
            angle = Constants.findAngle(new Vector2(0, -1), direction);

            //Move the boid
            Vector2 velocity = direction;
            velocity.Normalize();
            location += velocity * speed;
            
            //Reset the influence
            influenceVector.X = 0;
            influenceVector.Y = 0;
        }

        private void Cage()
        {
            if (location.X > borders.X - Constants.BORDER_DISTANCE) //Right side
            {
                Point influencePoint = new Point((int)(borders.X - Constants.BORDER_DISTANCE * 2), (int)location.Y);
                Point adjustedPoint = Constants.distanceAdjust(influencePoint, borders.X - Constants.BORDER_DISTANCE);
                adjustedPoint.Y = (int)location.Y;
                influenceBoid(influencePoint);
            }
            else if(location.X < Constants.BORDER_DISTANCE) //Left side
            {
                Point influencePoint = new Point((int)(Constants.BORDER_DISTANCE * 2), (int)location.Y);
                Point adjustedPoint = Constants.distanceAdjust(influencePoint, location.X);
                adjustedPoint.Y = (int)location.Y;
                influenceBoid(influencePoint);
            }
            else if(location.Y > borders.Y - Constants.BORDER_DISTANCE) //Bottom
            {
                Point influencePoint = new Point((int)location.X, (int)(borders.Y - Constants.BORDER_DISTANCE * 2));
                Point adjustedPoint = Constants.distanceAdjust(influencePoint, borders.Y - Constants.BORDER_DISTANCE);
                adjustedPoint.X = (int)location.X;
                influenceBoid(influencePoint);
            }
            else if(location.Y < Constants.BORDER_DISTANCE) //Top
            {
                Point influencePoint = new Point((int)(Constants.BORDER_DISTANCE * 2), (int)location.X);
                Point adjustedPoint = Constants.distanceAdjust(influencePoint, location.Y);
                adjustedPoint.X = (int)location.X;
                influenceBoid(influencePoint);
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

        protected bool isWithinBorders()
        {
            if (location.X > borders.X - Constants.BORDER_DISTANCE) return false; //Right side
            if (location.X < Constants.BORDER_DISTANCE) return false; //Left side
            if (location.Y > borders.Y - Constants.BORDER_DISTANCE) return false; //Bottom
            if (location.Y < Constants.BORDER_DISTANCE) return false; //Top
            return (true);
        }
    }
}
