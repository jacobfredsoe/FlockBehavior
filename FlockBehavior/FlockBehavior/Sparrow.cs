using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlockBehavior
{
    class Sparrow : Boid
    {

        public Sparrow(Texture2D texture, Vector2 location, float speed, float turnspeed, Vector2 borders)
            : base(texture, location, speed, turnspeed, borders)
        {

        }

        public void Update(GameTime gameTime, List<Boid> boids)
        {
            Boid closestBoid = boids[0];
            foreach (Boid boid in boids)
            {
                float distance = Vector2.Distance(this.Location, boid.Location);
                if (distance < Vector2.Distance(this.Location, closestBoid.Location) && boid != this)
                {
                    closestBoid = boid;
                }
            }

            if (Vector2.Distance(this.Location, closestBoid.Location) <= Constants.SPARROW_INFLUENCE_DISTANCE & isWithinBorders())
            {
                Vector2 directionVector = this.Location - closestBoid.Location;
                this.influenceBoid(new Point((int)(this.Location.X + directionVector.X), (int)(this.Location.Y + directionVector.Y)));
            }
            base.Update(gameTime);
        }
    }
}
